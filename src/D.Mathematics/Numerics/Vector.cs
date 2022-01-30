using System;

namespace E.Numerics;

public class Vector<T> : IObject
    where T : struct, IEquatable<T>, IFormattable
{
    private MathNet.Numerics.LinearAlgebra.Vector<T> impl;

    public Vector(T[] elements)
    {
        impl = MathNet.Numerics.LinearAlgebra.Vector<T>.Build.DenseOfArray(elements);
    }

    internal Vector(MathNet.Numerics.LinearAlgebra.Vector<T> impl)
    {
        this.impl = impl;
    }

    internal MathNet.Numerics.LinearAlgebra.Vector<T> BaseVector => impl;

    public int Count => impl.Count;

    ObjectType IObject.Kind => ObjectType.Vector;
}
