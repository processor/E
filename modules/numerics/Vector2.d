Vector2 struct {
  let x, y: Number
}

Vector2 impl for Vector {
  count => 2

  [index: i64] => match index { 
    | 0 => x
    | 1 => y
  }
}