None        struct @primitive @size(0)
Boolean     struct @primitive @size(1) 

// Integers                   
Int8        struct @primitive @size(1)  
Int16       struct @primitive @size(2)  
Int32       struct @primitive @size(4)  
Int64       struct @primitive @size(8)  
Int128      struct @primitive @size(16) 
Int1024     struct @primitive @size(128)
Int2048     struct @primitive @size(256)
Int4096     struct @primitive @size(512)

UInt8       struct @primitive @size(1)  // byte
UInt16      struct @primitive @size(2)  
UInt32      struct @primitive @size(4)  
UInt64      struct @primitive @size(8)  

// Numbers       
Float16     struct @primitive @size(2)  @epsilon(4.88e-04) 
Float32     struct @primitive @size(4)  @epsilon(5.96e-08) 
Float64     struct @primitive @size(8)  @epsilon(1.11e-16)
Float128    struct @primitive @size(16) @epsilon(9.63e-35)

// Decimals
Decimal64   struct @primitive @size(8)
Decimal128  struct @primitive @size(16)

// Vectors          
Vector128   struct @primitive @size(16)
Vector256   struct @primitive @size(32)
Vector512   struct @primitive @size(64)

UUID        struct @primitive @size(16)