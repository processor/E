using System.Threading;

namespace E.Units;

internal static class UnitId
{
    private static uint _sequenceNumber = 1_000_000_000;

    public static uint Next()
    {
        return Interlocked.Increment(ref _sequenceNumber);
    }
}