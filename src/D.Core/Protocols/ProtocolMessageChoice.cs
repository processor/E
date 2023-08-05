namespace E.Protocols;

public sealed class ProtocolMessageChoice : IProtocolMessage
{
    public ProtocolMessageChoice(
        ProtocolMessage[] options,
        ProtocolMessageFlags flags = default)
    {
        Options = options;
        Flags = flags;
    }
    public ProtocolMessage[] Options { get; }

    public ProtocolMessageFlags Flags { get; }

    public ProtocolMessage this[int index] => Options[index];

    public int Count => Options.Length;

    public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

    public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

    public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
}