namespace E;

public enum OperatorType : byte
{
    Prefix  = 1 , // --1,    Unary
    Postfix = 2,  // 1++     Unary
    Infix   = 3   // 1 + 1   Binary
}

// infix operator ^^ { associativity left precedence 160 }

// infix ^^ (x, y) := a ** b     