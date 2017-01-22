namespace D.Primitives
{
    public interface IGenerator2<T>
    {
        T Next(); // Change return to ValueTask<T>
    }
}