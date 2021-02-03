namespace E
{
    public interface IConverter<S, T>
    {
        T Convert(S value);
    }
}