using System;

namespace BlackJack.Core
{
    [Flags]
    public enum AvailableTurns
    {
        None = 0,
        Hit = 1,
        Stand = 2,
        Double = 4,
        Split = 8,
        Insurance = 16
    }
}
