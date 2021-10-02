namespace E.Primitives;

public interface IGenerator<T>
{
    T Next(); // Change return to ValueTask<T>
}