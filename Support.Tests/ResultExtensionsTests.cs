using System;
using System.Collections.Generic;

using FGS.Incipience.Support;

using Moq;

using NUnit.Framework;

using TestSupport;

namespace Support.Tests
{
    public class ResultExtensionsTests
    {
        private static readonly string FirstSuccess = "Success.";
        private static readonly string FirstError = "Error.";
        private static readonly string SecondSuccess = "Second success.";
        private static readonly string SecondError = "Second error.";

        private readonly IResult<IEnumerable<string>, string> _startingSuccessResult = Result.Success<IEnumerable<string>, string>(new[] { FirstSuccess });
        private readonly IResult<IEnumerable<string>, string> _startingErrorResult = Result.Error<IEnumerable<string>, string>(FirstError);
        private readonly IResult<string, string> _successResult = Result.Success<string, string>(SecondSuccess);
        private readonly IResult<string, string> _errorResult = Result.Error<string, string>(SecondError);

        private Mock<Func<string, IResult<string, string>>> _mockEvaluateIResultFunc;

        [SetUp]
        public void SetUp()
        {
            _mockEvaluateIResultFunc = new Mock<Func<string, IResult<string, string>>>();
        }

        [Test]
        public void CombineOnSuccessWith_TwoSuccess_CombineSuccessfully()
        {
            var result = _startingSuccessResult.CombineOnSuccessWith("", _ => _successResult);

            result.AssertSuccess(new[] { FirstSuccess, SecondSuccess });
        }

        [Test]
        public void CombineOnSuccessWith_SuccessAndError_ReturnsError()
        {
            var result = _startingSuccessResult.CombineOnSuccessWith("", _ => _errorResult);

            result.AssertError(SecondError);
        }

        [Test]
        public void CombineOnSuccessWith_ErrorAndSuccess_ReturnsError_AndDoesNotEvaluateSecondResult()
        {
            var result = _startingErrorResult.CombineOnSuccessWith("", _mockEvaluateIResultFunc.Object);

            result.AssertError(FirstError);
            _mockEvaluateIResultFunc.VerifyNoOtherCalls();
        }

        [Test]
        public void CombineOnSuccessWith_TwoErrors_ReturnsFirstError_AndDoesNotEvaluateSecondResult()
        {
            var result = _startingErrorResult.CombineOnSuccessWith("", _mockEvaluateIResultFunc.Object);

            result.AssertError(FirstError);
            _mockEvaluateIResultFunc.VerifyNoOtherCalls();
        }

        [Test]
        public void CombineOnSuccessWithMany_GivenManySuccesses_EvaluatesAndReturnsAllSuccesses_InOrder()
        {
            var thirdSuccess = "Third Success.";
            var fourthSuccess = "Fourth Success.";
            var thirdSuccessResult = Result.Success<string, string>(thirdSuccess);
            var fourthSuccessResult = Result.Success<string, string>(fourthSuccess);

            var operands = new[] { "", "", "" };
            _mockEvaluateIResultFunc.SetupSequence(m => m(It.IsAny<string>()))
                .Returns(_successResult)
                .Returns(thirdSuccessResult)
                .Returns(fourthSuccessResult);

            var result = _startingSuccessResult.CombineOnSuccessWithMany(operands, _mockEvaluateIResultFunc.Object);

            result.AssertSuccess(new[] { FirstSuccess, SecondSuccess, thirdSuccess, fourthSuccess });
            _mockEvaluateIResultFunc.Verify(m => m(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public void CombineOnSuccessWithMany_GivenAnError_ReturnsError_AndStopsEvaluatingIResultsAfterError()
        {
            var operands = new[] { "", "", "" };
            _mockEvaluateIResultFunc.SetupSequence(m => m(It.IsAny<string>()))
                .Returns(_successResult)
                .Returns(_errorResult)
                .Returns(_successResult);

            var result = _startingSuccessResult.CombineOnSuccessWithMany(operands, _mockEvaluateIResultFunc.Object);

            result.AssertError(SecondError);
            _mockEvaluateIResultFunc.Verify(m => m(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void ContinueOnSuccessWithMany_GivenError_ReturnsError()
        {
            var error = "Error.";

            var startingResult = Result.Error<string>(error);
            var operands = new[] { "" };
            var result = startingResult.ContinueOnSuccessWithMany(operands, _ => _successResult);

            result.AssertError(error);
        }

        [Test]
        public void ContinueOnSuccessWithMany_GivenSuccess_ReturnsSubsequentResults()
        {
            var startingResult = Result.Success<string>();
            var operands = new[] { "", "" };
            var result = startingResult.ContinueOnSuccessWithMany(operands, _ => _successResult);

            result.AssertSuccess(new[] { SecondSuccess, SecondSuccess });
        }
    }
}
