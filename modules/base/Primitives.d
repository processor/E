None        type @size(0)
Boolean     type @size(1) 

// Integers                   
Int8        type @size(1)  
Int16       type @size(2)  
Int32       type @size(4)  
Int64       type @size(8)  
Int128      type @size(16) 
Int1024     type @size(128)
Int2048     type @size(256)

// Numbers       
Float16     type @size(2)  @epsilon(4.88e-04) 
Float32     type @size(4)  @epsilon(5.96e-08) 
Float64     type @size(8)  @epsilon(1.11e-16)
Float128    type @size(16) @epsilon(9.63e-35)

// Decimals
Decimal64   type @size(8)
Decimal128  type @size(16)

// Vectors          
Vector128   type @size(16)
Vector256   type @size(32)
Vector512   type @size(64)