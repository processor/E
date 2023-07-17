namespace E.Protocols;

public sealed class ProtocolMessageChoice(
    ProtocolMessage[] options,
    ProtocolMessageFlags flags) : IProtocolMessage
{
    public ProtocolMessage[] Options { get; } = options;

    public ProtocolMessage this[int index] => Options[index];

    public int Count => Options.Length;

    public ProtocolMessageFlags Flags { get; } = flags;

    public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

    public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

    public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
}