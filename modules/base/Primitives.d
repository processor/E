None        type @primitive @size(0)
Boolean     type @primitive @size(1) 

// Integers                   
Int8        type @primitive @size(1)  
Int16       type @primitive @size(2)  
Int32       type @primitive @size(4)  
Int64       type @primitive @size(8)  
Int128      type @primitive @size(16) 
Int1024     type @primitive @size(128)
Int2048     type @primitive @size(256)
Int4096     type @primitive @size(512)

UInt8       type @primitive @size(1)  // byte
UInt16      type @primitive @size(2)  
UInt32      type @primitive @size(4)  
UInt64      type @primitive @size(8)  

// Numbers       
Float16     type @primitive @size(2)  @epsilon(4.88e-04) 
Float32     type @primitive @size(4)  @epsilon(5.96e-08) 
Float64     type @primitive @size(8)  @epsilon(1.11e-16)
Float128    type @primitive @size(16) @epsilon(9.63e-35)

// Decimals
Decimal64   type @primitive @size(8)
Decimal128  type @primitive @size(16)

// Vectors          
Vector128   type @primitive @size(16)
Vector256   type @primitive @size(32)
Vector512   type @primitive @size(64)