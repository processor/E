namespace D
{
    public enum ParameterFlags : byte
    {
        None     = 0,
        Optional = 1 << 0,
        ReadOnly = 1 << 1
    }
}