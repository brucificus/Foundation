using System;

using FGS.Incipience.Support.Abstractions;

namespace FGS.Incipience.Support
{
    internal class Clock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
