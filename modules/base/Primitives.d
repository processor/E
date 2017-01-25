Boolean    primitive { size: 1   }

// Integers
Int8       primitive { size: 1   }
Int16      primitive { size: 2   }
Int32      primitive { size: 4   }
Int64      primitive { size: 8   }
Int128     primitive { size: 16  }
Int1024    primitive { size: 128 }
Int2048    primitive { size: 256 }
  
// Floats
Float16    primitive { size:  2, epsilon: 4.88e-04 } 
Float32    primitive { size:  4, epsilon: 5.96e-08 }   
Float64    primitive { size:  8, epsilon: 1.11e-16 } 
Float80    primitive { size: 10, epsilon: 5.42e-20 }
Float128   primitive { size: 16, epsilon: 9.63e-35 }

// Decimals
Decimal64  primitive { size: 8 }
Decimal128 primitive { size: 16 }

// Vectors
Vector128  primitive { size: 16 }
Vector256  primitive { size: 32 }
Vector512  primitive { size: 64 }