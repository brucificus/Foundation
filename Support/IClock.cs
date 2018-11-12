using System;

namespace Support
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}
