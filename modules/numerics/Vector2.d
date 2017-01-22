Vector2 type { 
  x, y: Float
}

Vector2 impl {
  from (x, y) => Vector2 { x, y }
}

Vector2 impl for Vector {
  count => 2

  [index: Integer] => match index { 
    | 0 => x
    | 1 => y
  }
}