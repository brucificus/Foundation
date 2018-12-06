using System.Collections.Generic;
using System.Collections.Immutable;

namespace FGS.Incipience.Support
{
    public static class EnumerableExtensions
    {
        // ReSharper disable once ImpureMethodCallOnReadonlyValueField
        public static IEnumerable<T> Yield<T>(this T self) => ImmutableArray<T>.Empty.Add(self);
    }
}
