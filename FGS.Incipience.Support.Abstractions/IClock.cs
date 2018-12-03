using System;

namespace FGS.Incipience.Support.Abstractions
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}
