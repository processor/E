namespace E.Protocols;

public sealed class ProtocolMessageChoice : IProtocolMessage
{
    public ProtocolMessageChoice(ProtocolMessage[] options, ProtocolMessageFlags flags)
    {
        Options = options;
        Flags = flags;
    }

    public ProtocolMessage[] Options { get; }

    public ProtocolMessage this[int index] => Options[index];

    public int Count => Options.Length;

    public ProtocolMessageFlags Flags { get; }

    public bool Fallthrough => Flags.HasFlag(ProtocolMessageFlags.Fallthrough);

    public bool Repeats => Flags.HasFlag(ProtocolMessageFlags.Repeats);

    public bool IsEnd => Flags.HasFlag(ProtocolMessageFlags.End);
}