using System;

namespace FGS.Incipience.Support
{
    public class Clock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
