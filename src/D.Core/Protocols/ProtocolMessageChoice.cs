namespace E.Protocols;

public sealed class ProtocolMessageChoice(
    ProtocolMessage[] options,
    ProtocolMessageFlags flags = default) : IProtocolMessage
{
    public ProtocolMessage[] Options { get; } = options;

    public ProtocolMessageFlags Flags { get; } = flags;

    public ProtocolMessage this[int index] => Options[index];

    public int Count => Options.Length;

    public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

    public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

    public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
}