using System.Linq;

using NUnit.Framework;

namespace FGS.Incipience.Support.Tests
{
    public class StringExtensionsTests
    {
        [TestCase(" A \nB")]
        [TestCase("A\r B ")]
        [TestCase("A\r\nB")]
        [TestCase("\r\nA\n\r\r\n\nB\n")]
        public void SplitAndTrimOnAllNewlines_SplitsOnAllNewlines_AndIgnoresEmptyStrings(string input)
        {
            var result = input.SplitAndTrimOnAllNewlines();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SplitAndTrimOnAllNewlines_TrimsWhitespaceOnAllSegments()
        {
            var result = "   A \n   B  ".SplitAndTrimOnAllNewlines();
            Assert.That(result.First(), Is.EqualTo("A"));
            Assert.That(result.Last(), Is.EqualTo("B"));
        }
    }
}
