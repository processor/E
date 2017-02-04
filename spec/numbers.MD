# Numbers

## Literals


Optional exponent.
Optional _ seperators.

By default, numeric literals are assumed to be Float64.

```
let a = 1      # Float64
let b: i32 = 1 # Int32
```

### Encodings

**Hexidecimal**

0x followed by a sequence of [0-9a-f] pairs, optionally seperated by underscores.

```
0x_a9_12_ff
```

*open* support capital letters?

**Octal**

0x followed by a sequence of digits between [0-7], optionally seperated by underscores.

```
 0o135
```

**Binary**

0x followed by a sequence of 0 and 1's, optionally seperated by underscores.

```
0b00100011
```

## Integers

```
i8  Int8
i16 Int16
i32 Int32
i16 Int64

ui8  UInt8
ui16 UInt16
ui32 UInt32
ui64 UInt64
```

## Floats

Floats contain a decimal point or exponent.

```
f16 Float16
f32 Float32
f64 Float64
```

## Decimals

```
dec64 Dec64
d64   Decimal64
d128  Decimal128
```
