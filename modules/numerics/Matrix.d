Matrix<T: Number & Blittable> protocol {
  [ index: i64 ]            -> T
  [ row: i64, column: i64 ] -> T

  transpose() -> Matrix<T>
}

// determinant -> T
// inverse
// outerProduct
// componentWiseMultiply