abs      ƒ (x: Float64) -> Float64;                   // Absolute value
abs      ƒ (x: Int64)   -> Int64;                   

// Rounding
round    ƒ(x: Float64)                      -> Float64;
round    ƒ(x: Float64, digits: i64)         -> Float64;
round    ƒ(x: Float64, digits: i64, Midway) -> Float64;
ceiling  ƒ(x: Float64) -> Float64;                                                    // ceil in JS
floor    ƒ(x: Float64) -> Float64;
truncate ƒ(x: Float64) -> Float64;

// Clamping
clamp ƒ(x: Float64, min: Float64, max: Float64) -> Float64;
clamp ƒ(x: Int64  , min: Int64  , max: Int64)   -> Int64;

// Signs
sign ƒ(x: Float64) -> Float64;       // 1 when positive, 0 when 0, - 1 when negitive
sign ƒ(x: Int64)   -> Int64;

// Min / max
min  ƒ(x: Self, y: Self) where Self : Comparable -> Self;
max  ƒ(x: Self, y: Self) where Self : Comparable -> Self;

// Logarithmic functions
log   ƒ(x: Float64) -> Float64;    // the power to which the constant e has to be raised to produce x.
log2  ƒ(x: Float64) -> Float64;    
log10 ƒ(x: Float64) -> Float64;

// Exponentiation functions
pow   ƒ(x: Float64, exponent: Float64) -> Float64;            // exponentiation
sqrt  ƒ(x: Float64) -> Float64;                              // square root
cbrt  ƒ(x: Float64) -> Float64;                              // cube root
exp   ƒ(x: Float64) -> Float64;                              // exponential      -- the constant e raised to the power of x.
exp2  ƒ(x: Float64) => pow(2, x)

mod   ƒ(x: Float64, y: Float64) -> Float64;
                                                                          
// Trigonometric functions
sin   ƒ(x: Float64) -> Float64;                                        // sine                    
cos   ƒ(x: Float64) -> Float64;                                        // cosine                  
tan   ƒ(x: Float64) -> Float64;                                        // tangent                 
cot   ƒ(x: Float64) -> Float64;                                        // cotangent
sec   ƒ(x: Float64) => 1 / cos(x);                                     // secant
csc   ƒ(x: Float64) => 1 / sin(x);                                     // cosecant
cotan ƒ(x: Float64) => 1 / tan(x);                                     // co-tangent

// Hyperbolic functions
sinh   ƒ(x: Float64) => (exp(x) - exp(-x)) / 2;                        // hyperbolic sine         
cosh   ƒ(x: Float64) => (exp(x) + exp(-x)) / 2;                        // hyperbolic cosine       
tanh   ƒ(x: Float64) => (exp(x) - exp(-x)) / (exp(x) + exp(-x));       // hyperbolic tangent      
coth   ƒ(x: Float64) -> Float64;                                        // hyperbolic cotangent   
sech   ƒ(x: Float64) => 2 / (exp(x) + exp(-x));                        // hyperbolic secant
csch   ƒ(x: Float64) => 2 / (exp(x) - exp(-x));                        // hyperbolic cosecant

asin   ƒ(x: Float64) => atan(x / sqrt(-x * x + 1));                    // arc-sine                
acos   ƒ(x: Float64) => atan(-x / sqrt(-x * x + 1)) + 2 * Atn(1);      // arc-cosine              
atan   ƒ(x: Float64) ->  T;                                            // arc-tangent             
acot   ƒ(x: Float64) => (exp(x) + exp(-x)) / (exp(x) - exp(-x));       // arc-cotangent
asec   ƒ(x: Float64) => 2 * atan(1) - atan(sign(x) / sqrt(x * x - 1)); // arc-secant
acsc   ƒ(x: Float64) => atan(sign(x) / sqrt(x * x - 1));               // arc-cosecant
acotan ƒ(x: Float64) => 2 * atan(1) - atan(x); 

asinh  ƒ(x: Float64) => log(x + sqrt(x * x + 1));                      // hyperbolic arc-sine     
acosh  ƒ(x: Float64) => log(sqrt(x * x - 1) + x);                      // hyperbolic arc-cosine   
atanh  ƒ(x: Float64) => log(sqrt(-x * x + 1) / x);                     // hyperbolic arc-tangent  
acoth  ƒ(x: Float64) => log((x + 1) / x) + log(x / (x - 1)) / 2;       // hyperbolic arc-cotangent
asech  ƒ(x: Float64) => log(sqrt(x * x - 1) + 1);                      // hyperbolic arc-secant   
acsch  ƒ(x: Float64) => log(x + sqrt(x * x + 1));                      // hyperbolic arc-cosecant 

atan2  ƒ(x: Float64, y: Float64) -> Float64;                                              

// inversesqrt   // inverse square root (opengl)

// fma // Fused Mutiply Add
