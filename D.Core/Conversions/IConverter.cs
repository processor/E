namespace D
{
    public interface IConverter<S, T>
    {
        T Convert(S value);
    }
}