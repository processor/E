Matrix<T> protocol {
  [ index: Int64 ]              -> T
  [ row: Int64, column: Int64 ] -> T

  transpose() -> Matrix<T>
}

// determinant -> T
// inverse
// outerProduct
// componentWiseMultiply