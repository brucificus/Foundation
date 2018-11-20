using FGS.Incipience.Support;

using NUnit.Framework;

namespace FGS.Incipience.TestSupport
{
    public static class ResultExtensions
    {
        public static void AssertSuccess<TError>(
            this IResult<TError> result)
        {
            result.Match(
                success: Assert.Pass,
                error: data => Assert.Fail("Expected success result, but was error"));
        }

        public static void AssertError<TError>(
            this IResult<TError> result,
            TError expectedErrorData)
        {
            result.Match(
                success: () => Assert.Fail("Expected error result, but was success"),
                error: data => Assert.That(data, Is.EqualTo(expectedErrorData)));
        }

        public static void AssertError<TSuccess, TError>(this IResult<TSuccess, TError> result, TError expectedErrorData)
        {
            result.Match(
                success: data => Assert.Fail("Expected expectedErrorData result, but was success"),
                error: data => Assert.That(data, Is.EqualTo(expectedErrorData)));
        }

        public static void AssertSuccess<TSuccess, TError>(
            this IResult<TSuccess, TError> result,
            TSuccess expectedSuccessData)
        {
            result.Match(
                success: data => Assert.That(data, Is.EqualTo(expectedSuccessData)),
                error: data => Assert.Fail("Expected success result, but was expectedErrorData"));
        }
    }
}
