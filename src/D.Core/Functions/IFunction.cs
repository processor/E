namespace E
{
    public interface IFunction : INamedObject
    { 
        Parameter[] Parameters { get; }

        object Invoke(IArguments args);
    }
}