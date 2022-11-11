using System.Collections.Generic;

namespace E;

public interface IArguments : IEnumerable<Argument>
{
    object this[int i] { get; }

    object? this[string name] { get; }

    int Count { get; }
}