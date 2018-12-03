using System;
using System.Collections.Generic;
using System.Linq;

using FGS.Incipience.Support.Abstractions;

namespace FGS.Incipience.Support
{
    public static class ResultExtensions
    {
        private static IResult<IEnumerable<TSuccessData>, TError> CombineOnSuccessWith<TSuccessData, TError>(
            this IResult<IEnumerable<TSuccessData>, TError> result,
            Lazy<IResult<TSuccessData, TError>> lazySecondResult)
        {
            return result.Match(
                success: successes =>
                {
                    return lazySecondResult.Value.Match(
                        success: innerData =>
                        {
                            return Result.Success<IEnumerable<TSuccessData>, TError>(successes.Append(innerData));
                        },
                        error: Result.Error<IEnumerable<TSuccessData>, TError>);
                },
                error: Result.Error<IEnumerable<TSuccessData>, TError>);
        }

        public static IResult<IEnumerable<TSuccessData>, TError> CombineOnSuccessWith<TStartType, TSuccessData, TError>(
            this IResult<IEnumerable<TSuccessData>, TError> result,
            TStartType operand,
            Func<TStartType, IResult<TSuccessData, TError>> conversionFunc)
        {
            var l = new Lazy<IResult<TSuccessData, TError>>(() => conversionFunc(operand));
            return CombineOnSuccessWith(result, l);
        }

        public static IResult<IEnumerable<TSuccessData>, TError> CombineOnSuccessWithMany<TStartType, TSuccessData, TError>(
            this IResult<IEnumerable<TSuccessData>, TError> result,
            IEnumerable<TStartType> operands,
            Func<TStartType, IResult<TSuccessData, TError>> conversionFunc)
        {
            if (!operands.Any()) return result;

            var intermediateResult = CombineOnSuccessWith(result, operands.First(), conversionFunc);
            return intermediateResult.CombineOnSuccessWithMany(operands.Skip(1), conversionFunc);
        }

        public static IResult<IEnumerable<TSuccessData>, TError> ContinueOnSuccessWithMany<TStartType, TSuccessData, TError>(
            this IResult<TError> result,
            IEnumerable<TStartType> operands,
            Func<TStartType, IResult<TSuccessData, TError>> conversionFunc)
        {
            return result.Match(
                success: () =>
                {
                    var starting = Result.Success<IEnumerable<TSuccessData>, TError>(Enumerable.Empty<TSuccessData>());
                    return starting.CombineOnSuccessWithMany(operands, conversionFunc);
                },
                error: Result.Error<IEnumerable<TSuccessData>, TError>);
        }
    }
}
