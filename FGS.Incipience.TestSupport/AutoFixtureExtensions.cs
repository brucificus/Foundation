using AutoFixture;

using Moq;

namespace FGS.Incipience.TestSupport
{
    public static class AutoFixtureExtensions
    {
        public static Mock<T> Mock<T>(this Fixture fixture)
            where T : class
        {
            var mock = new Mock<T>();
            fixture.Register(() => mock.Object);
            return mock;
        }
    }
}
