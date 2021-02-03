namespace E
{
    public enum RangeFlags : byte
    {
        Inclusive = 1, // Includes first and last
        Exlusive  = 2, // Excludes first and last
        HalfOpen  = 3  // Includes first, but not last
    }
}