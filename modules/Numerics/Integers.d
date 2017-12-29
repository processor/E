i8    struct @primitive @size(1)   : Integer
i16   struct @primitive @size(2)   : Integer
i32   struct @primitive @size(4)   : Integer
i64   struct @primitive @size(8)   : Integer
i128  struct @primitive @size(16)  : Integer
i1024 struct @primitive @size(128) : Integer
i2048 struct @primitive @size(256) : Integer
i4096 struct @primitive @size(512) : Integer

// The LLVM language specifies integer types as iN, where N is the bit-width of the integer, and ranges from 1 to 2^23-1