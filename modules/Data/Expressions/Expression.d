Expression class {

  Constant() { } 
  Equal ($0: Expression, $1: Expression)
  Not   ($0: Expression) -> Unary

  Unary { expression: Expression, operation: Operation } 

  Binary { 
    lhs      : Expression
    rhs      : Expression
    operator : Operator
  } 
  
  Ternary { }


}

// Expression::Constant
