namespace D
{
    public interface IFunction : INamedObject
    { 
        Parameter[] Parameters { get; }

        IObject Invoke(IArguments args);
    }
}