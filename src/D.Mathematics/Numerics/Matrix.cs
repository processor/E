using System;

namespace E.Numerics
{
    using Expressions;

    public class Matrix<T> : IObject
        where T : struct, IComparable<T>, IEquatable<T>, IFormattable
    {
        private MathNet.Numerics.LinearAlgebra.Matrix<T> impl;

        public Matrix(T[][] rows)
        {
            impl = MathNet.Numerics.LinearAlgebra.Matrix<T>.Build.SparseOfRowArrays(rows);
        }

        private Matrix(MathNet.Numerics.LinearAlgebra.Matrix<T> impl)
        {
            this.impl = impl;
        }

        public T this[int x, int y] => impl[x, y];

        public int RowCount => impl.RowCount;

        public int ColumnCount => impl.ColumnCount;

        public int ElementCount => RowCount * ColumnCount;

        #region IArithmetic / Scalars

        public IObject Add(INumber scalar)
            => new Matrix<T>(impl.Add(scalar.As<T>()));

        public IObject Subtract(INumber scalar)
            => new Matrix<T>(impl.Subtract(scalar.As<T>()));

        public IObject Divide(INumber scalar)
            => new Matrix<T>(impl.Divide(scalar.As<T>()));

        public IObject Mutiply(INumber scalar)
            => new Matrix<T>(impl.Multiply(scalar.As<T>()));

        #endregion

        #region IArithmetic / Matrixes

        public Matrix<T> Multiply(Matrix<T> other)
        {
            var result = impl.Multiply(other.impl);

            return new Matrix<T>(result);
        }

        public Matrix<T> Add(Matrix<T> other)
        {
            var result = impl.Add(other.impl);

            return new Matrix<T>(result);
        }

        public Matrix<T> Subtract(Matrix<T> other)
        {
            var result = impl.Subtract(other.impl);

            return new Matrix<T>(result);
        }

        #endregion

        #region Operators

        public static Matrix<T> operator +(Matrix<T> l, T r)
            => new Matrix<T>(l.impl + r);

        public static Matrix<T> operator +(Matrix<T> l, Matrix<T> r)
            => new Matrix<T>(l.impl + r.impl);

        public static Matrix<T> operator +(T l, Matrix<T> r)
           => new Matrix<T>(l + r.impl);

        public static Matrix<T> operator -(Matrix<T> l, Matrix<T> r)
           => new Matrix<T>(l.impl - r.impl);

        public static Matrix<T> operator -(T l, Matrix<T> r)
          => new Matrix<T>(l - r.impl);

        public static Matrix<T> operator -(Matrix<T> l, T r)
        {
            return new Matrix<T>(l.impl - r);
        }

        public static Matrix<T> operator *(Matrix<T> l, Matrix<T> r)
           => new Matrix<T>(l.impl * r.impl);

        public static Matrix<T> operator *(T l, Matrix<T> r)
           => new Matrix<T>(l * r.impl);

        public static Matrix<T> operator *(Matrix<T> l, T r)
            => new Matrix<T>(l.impl * r);

        public static Vector<T> operator *(Matrix<T> l, Vector<T> r)
            => new Vector<T>(l.impl * r.BaseVector);

        public static Matrix<T> operator /(Matrix<T> dividend, T divisor)
            => new Matrix<T>(dividend.impl / divisor);

        public static Matrix<T> operator /(T dividend, Matrix<T> divisor)
        {
            return new Matrix<T>(dividend / divisor.impl);
        }

        public static Matrix<T> operator %(T dividend, Matrix<T> divisor)
           => new Matrix<T>(dividend % divisor.impl);

        public static Matrix<T> operator %(Matrix<T> dividend, Matrix<T> divisor)
            => new Matrix<T>(dividend.impl % divisor.impl);

        public static Matrix<T> operator %(Matrix<T> dividend, T divisor)
            => new Matrix<T>(dividend.impl % divisor);

        #endregion        

        /*
        [ 
          [ 1, 2, 3],
          [ 4, 5, 6] 
        ] 
        */
        public static Matrix<T> Create(ArrayInitializer expression)
        {
            if (expression.Stride is null)
                throw new Exception("Missing stride");

            var stride = expression.Stride.Value;
            
            var rows = new T[expression.Elements.Length][];

            var rI = 0;
            
            foreach (var row in expression.Elements)
            {
                var i = 0;

                var a = ((ArrayInitializer)row);

                if (a.Elements.Length != stride) throw new Exception("invalid row lenth");

                var b = new T[a.Elements.Length];

                foreach (var column in a.Elements)
                {
                    b[i] = ((INumber)column).As<T>();

                    i++;
                }

                rows[rI] = b;

                rI++;
            }

            return new Matrix<T>(rows);
        }

        ObjectType IObject.Kind => ObjectType.Matrix;
    }
}
