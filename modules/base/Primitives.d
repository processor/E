none    struct @primitive @size(0)
boolean struct @primitive @size(1) 

// Integers                   
i8     struct @primitive @size(1)  
i16    struct @primitive @size(2)  
i32    struct @primitive @size(4)  
i64    struct @primitive @size(8)  
i128   struct @primitive @size(16) 
i1024  struct @primitive @size(128)
i2048  struct @primitive @size(256)
i4096  struct @primitive @size(512)

// Numbers       
f16     struct @primitive @size(2)  @epsilon(4.88e-04) 
f32     struct @primitive @size(4)  @epsilon(5.96e-08) 
f64     struct @primitive @size(8)  @epsilon(1.11e-16)
f128    struct @primitive @size(16) @epsilon(9.63e-35)

// Decimals
d64   struct @primitive @size(8)
d128  struct @primitive @size(16)

// Vectors          
v128   struct @primitive @size(16)
v256   struct @primitive @size(32)
v512   struct @primitive @size(64)

UUID   struct @primitive @size(16)

// Pointer
// NativeInteger