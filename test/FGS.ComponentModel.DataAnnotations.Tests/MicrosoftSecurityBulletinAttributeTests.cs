using System.Collections.Generic;

using AutoFixture;

using FGS.Tests.Support.TestCategories;

using NUnit.Framework;

namespace FGS.ComponentModel.DataAnnotations.Tests
{
    [Unit]
    [TestFixture]
    public class MicrosoftSecurityBulletinAttributeTests
    {
        private MicrosoftSecurityBulletinAttribute _subject;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _subject = fixture.Create<MicrosoftSecurityBulletinAttribute>();
        }

        [TestCaseSource(nameof(FailureCases))]
        public void IsValid_GivenFailureCases_ReturnsFalse(string bulletinList)
        {
            var results = _subject.IsValid(bulletinList);

            Assert.False(results);
        }

        [TestCaseSource(nameof(SuccessCases))]
        public void IsValid_GivenSuccessCases_ReturnsTrue(string bulletinList)
        {
            var results = _subject.IsValid(bulletinList);

            Assert.True(results);
        }

        public static IEnumerable<string> FailureCases()
        {
            yield return "MS123-45";
            yield return "MS1-2345";
            yield return "12-345";
            yield return "MS-12-345";
            yield return "MS12345";
            yield return "MS12-3456";
            yield return "MS12-345, MS11234";
            yield return "MS12-345, MS-54-321";
            yield return "MS12-345,MS11234";
            yield return "MS13";
            yield return "SM12-345";
            yield return "MS12-345, MS13-456";
            yield return "MS12-345,MS13-456";
        }

        public static IEnumerable<string> SuccessCases()
        {
            yield return "MS12-345";
            yield return "MS13-456";
        }
    }
}
