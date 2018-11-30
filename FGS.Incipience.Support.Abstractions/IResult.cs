using System;

namespace FGS.Incipience.Support.Abstractions
{
#pragma warning disable SA1402 // File may only contain a single class
    public interface IResult<out TError>
    {
        void Match(Action success, Action<TError> error);

        TResult Match<TResult>(Func<TResult> success, Func<TError, TResult> error);
    }

    public interface IResult<out TSuccess, out TError>
    {
        void Match(Action<TSuccess> success, Action<TError> error);

        TResult Match<TResult>(Func<TSuccess, TResult> success, Func<TError, TResult> error);
    }

    public sealed class Success<TError> : IResult<TError>
    {
        public void Match(Action success, Action<TError> error) => success();

        public TResult Match<TResult>(Func<TResult> success, Func<TError, TResult> error) => success();
    }

    public sealed class Success<TSuccess, TError> : IResult<TSuccess, TError>
    {
        private readonly TSuccess _data;

        internal Success(TSuccess data)
        {
            _data = data;
        }

        public void Match(Action<TSuccess> success, Action<TError> error) => success(_data);

        public TResult Match<TResult>(Func<TSuccess, TResult> success, Func<TError, TResult> error) => success(_data);
    }

    public sealed class Error<TError> : IResult<TError>
    {
        private readonly TError _error;

        internal Error(TError error)
        {
            _error = error;
        }

        public void Match(Action success, Action<TError> error) => error(_error);

        public TResult Match<TResult>(Func<TResult> success, Func<TError, TResult> error) => error(_error);
    }

    public sealed class Error<TSuccess, TError> : IResult<TSuccess, TError>
    {
        private readonly TError _error;

        internal Error(TError error)
        {
            _error = error;
        }

        public void Match(Action<TSuccess> success, Action<TError> error) => error(_error);

        public TResult Match<TResult>(Func<TSuccess, TResult> success, Func<TError, TResult> error) => error(_error);
    }

    public static class Result
    {
        public static IResult<TError> Success<TError>() => new Success<TError>();

        public static IResult<TError> Error<TError>(TError error) => new Error<TError>(error);

        public static IResult<TSuccess, TError> Success<TSuccess, TError>(TSuccess data) => new Success<TSuccess, TError>(data);

        public static IResult<TSuccess, TError> Error<TSuccess, TError>(TError error) => new Error<TSuccess, TError>(error);
    }
#pragma warning restore SA1402 // File may only contain a single class
}