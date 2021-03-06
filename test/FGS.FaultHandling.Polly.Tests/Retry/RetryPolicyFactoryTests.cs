using System;
using System.Linq;

using AutoFixture;

using FGS.FaultHandling.Abstractions;
using FGS.FaultHandling.Abstractions.Retry;
using FGS.FaultHandling.Polly.Retry;
using FGS.Tests.Support;
using FGS.Tests.Support.AutoFixture.Mocking.Options;
using FGS.Tests.Support.TestCategories;

using NUnit.Framework;

using Polly;

namespace FGS.FaultHandling.Polly.Tests.Retry
{
    [Unit]
    [TestFixture]
    public class RetryPolicyFactoryTests
    {
        private Fixture _fixture;

        private Func<ISyncPolicy, IAsyncPolicy, IRetryPolicy> _retryPolicyWrapperHook;

        private RetryPolicyFactory _subject;

        [SetUp]
        public void Setup()
        {
            _fixture = AutoFixtureFactory.Create();

            _retryPolicyWrapperHook = (syncPolicy, asyncPolicy) => default(IRetryPolicy);

            _fixture.Register<Func<ISyncPolicy, IAsyncPolicy, IRetryPolicy>>(() => ((syncPolicy, asyncPolicy) => _retryPolicyWrapperHook(syncPolicy, asyncPolicy)));

            _fixture.RegisterMockOptions<FaultHandlingConfiguration>();

            _subject = _fixture.Create<RetryPolicyFactory>();
        }

        [Test]
        public void Create_GivenNoExceptionPredicates_ThrowsAnArgumentException()
        {
            var exceptionPredicates = Enumerable.Empty<Func<Exception, bool>>();

            Assert.Throws<ArgumentException>(() => _subject.Create(exceptionPredicates));
        }

        [Test]
        public void Create_GivenExceptionPredicates_UsesAPollyRetryPolicy()
        {
            var exceptionPredicates = _fixture.CreateMany<Func<Exception, bool>>();

            _retryPolicyWrapperHook = (syncPolicy, asyncPolicy) =>
            {
                Assert.That(syncPolicy, Is.InstanceOf<global::Polly.Retry.IRetryPolicy>());
                Assert.That(asyncPolicy, Is.InstanceOf<global::Polly.Retry.IRetryPolicy>());
                return default(IRetryPolicy);
            };

            _subject.Create(exceptionPredicates);
        }
    }
}
