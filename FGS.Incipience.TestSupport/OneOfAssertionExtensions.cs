using NUnit.Framework;
using OneOf;
using OneOf.Types;

namespace FGS.Incipience.TestSupport
{
    public static class OneOfAssertionExtensions
    {
        public static void AssertSuccess<TError>(
            this OneOf<Success, TError> result)
        {
            result.Switch(
                _ => Assert.Pass(),
                _ => Assert.Fail("Expected success result, but was error"));
        }

        public static void AssertError<TError>(
            this OneOf<Success, TError> result,
            TError expectedErrorData)
        {
            result.Switch(
                _ => Assert.Fail("Expected error result, but was success"),
                data => Assert.That(data, Is.EqualTo(expectedErrorData)));
        }

        public static void AssertError<TSuccess, TError>(this OneOf<TSuccess, TError> result, TError expectedErrorData)
        {
            result.Switch(
                _ => Assert.Fail("Expected error result, but was success"),
                data => Assert.That(data, Is.EqualTo(expectedErrorData)));
        }

        public static void AssertSuccessString<TError>(
            this OneOf<Success<string>, TError> result,
            string expectedSuccess)
        {
            result.Switch(
                data => Assert.That(data.Value, Is.EqualTo(expectedSuccess)),
                _ => Assert.Fail("Expected success result, but was error"));
        }

        public static void AssertSuccess<TSuccess, TError>(
            this OneOf<TSuccess, TError> result,
            TSuccess expectedSuccessData)
        {
            result.Switch(
                data => Assert.That(data, Is.EqualTo(expectedSuccessData)),
                _ => Assert.Fail("Expected success result, but was error"));
        }
    }
}
