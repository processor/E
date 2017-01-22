namespace D
{
    public interface IFunction : INamedObject
    {
        // Signature

        // arity (parameter count)

        Parameter[] Parameters { get; }

        IObject Invoke(IArguments args);
    }
}