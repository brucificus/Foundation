using System;

namespace Support
{
    public class Clock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
