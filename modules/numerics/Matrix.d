Matrix<T> protocol { 
  [ index: i64 ] -> T
  [ row: i64, column: i64 ] -> T


  transpose() -> Matrix<T>

}

determinant -> Number
inverse
outerProduct
componentWiseMultiply