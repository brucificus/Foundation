using System;

namespace FGS.Incipience.Support
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}
