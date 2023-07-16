namespace E.Protocols;

public sealed class ProtocolMessage(string name, string? label, ProtocolMessageFlags flags) : IProtocolMessage
{
    public string Name { get; } = name;

    public string? Label { get; } = label;

    public ProtocolMessageFlags Flags { get; } = flags;

    public bool IsOptional => Flags.HasFlag(ProtocolMessageFlags.Optional);

    public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

    public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

    public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
}