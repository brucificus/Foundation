using System;

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
