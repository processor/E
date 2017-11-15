i8    struct @primitive @size(1)  
i16   struct @primitive @size(2)  
i32   struct @primitive @size(4)  
i64   struct @primitive @size(8)  
i128  struct @primitive @size(16) 
i1024 struct @primitive @size(128)
i2048 struct @primitive @size(256)
i4096 struct @primitive @size(512)

// The LLVM language specifies integer types as iN, where N is the bit-width of the integer, and ranges from 1 to 2^23-1