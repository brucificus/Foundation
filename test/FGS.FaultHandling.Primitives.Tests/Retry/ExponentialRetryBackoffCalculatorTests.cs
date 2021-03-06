using System;

using AutoFixture;

using FGS.FaultHandling.Primitives.Retry;
using FGS.Tests.Support;
using FGS.Tests.Support.TestCategories;

using NUnit.Framework;

namespace FGS.FaultHandling.Primitives.Tests.Retry
{
    [Unit]
    [TestFixture]
    public class ExponentialRetryBackoffCalculatorTests
    {
        private Fixture _fixture;
        private ExponentialRetryBackoffCalculator _subject;

        [SetUp]
        public void Setup()
        {
            _fixture = AutoFixtureFactory.Create();

            _subject = _fixture.Create<ExponentialRetryBackoffCalculator>();
        }

        [Test]
        public void CalculateBackoff_ReturnsAnExponentialBackoffBasedOnTheRetryAttempt()
        {
            var retryAttempt = _fixture.CreateSmallPositiveRandomNumber();
            var expectedBackoffSeconds = Math.Pow(2, retryAttempt - 1);

            var backoff = _subject.CalculateBackoff(retryAttempt);

            Assert.That(backoff.TotalSeconds, Is.EqualTo(expectedBackoffSeconds));
        }
    }
}
