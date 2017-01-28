using System;

namespace D.Numerics
{
    using Expressions;

    public class Matrix<T> : IObject
        where T : struct, IEquatable<T>, IFormattable
    {
        private MathNet.Numerics.LinearAlgebra.Matrix<T> impl;

        public Matrix(T[] elements, int stride)
        {
            int rows = elements.Length / stride;

            impl = MathNet.Numerics.LinearAlgebra.Matrix<T>.Build.Dense(rows, stride, elements);
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
        public static Matrix<T> Create(NewArrayExpression expression)
        {
            #region Preconditions

            if (expression.Stride == null)
                throw new Exception("Missing stride");

            #endregion

            var rows = expression.Elements.Length;
            var stride = expression.Stride.Value;
            
            var elements = new T[rows * stride];

            var i = 0;

            foreach (var row in expression.Elements)
            {
                var r = ((NewArrayExpression)row);

                if (r.Elements.Length != stride) throw new Exception("invalid row lenth");

                foreach (var column in r.Elements)
                {
                    elements[i] = ((INumber)column).As<T>();

                    i++;
                }
            }

            return new Matrix<T>(elements, stride);
        }

        Kind IObject.Kind => Kind.Matrix;
    }
}
