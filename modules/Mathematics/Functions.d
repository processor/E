abs      ƒ (x: f64) -> f64;                   // Absolute value
abs      ƒ (x: i64) -> i64;                   

// Rounding
round    ƒ(x: f64)                      -> f64;
round    ƒ(x: f64, digits: i64)         -> f64;
round    ƒ(x: f64, digits: i64, Midway) -> f64;
ceiling  ƒ(x: f64) -> f64;                                                    // ceil in JS
floor    ƒ(x: f64) -> f64;
truncate ƒ(x: f64) -> f64;

// Clamping
clamp ƒ(x: f64, min: f64, max: f64) -> f64;
clamp ƒ(x: i64, min: i64, max: i64) -> i64;

// Signs
sign ƒ(x: f64) -> f64;       // 1 when positive, 0 when 0, - 1 when negitive
sign ƒ(x: i64) -> i64;

// Min / max
min  ƒ(x: Self, y: Self) where Self : Comparable -> Self;
max  ƒ(x: Self, y: Self) where Self : Comparable -> Self;

min  ƒ(values: Self*) where Self : Comparable -> Self;
max  ƒ(values: Self*) where Self : Comparable -> Self;

// Exponentiation functions
pow   intrinsic ƒ(x: f64, exponent: f64) -> f64;              // exponentiation
sqrt  intrinsic ƒ(x: f64) -> f64;                             // square root
cbrt  intrinsic ƒ(x: f64) -> f64;                             // cube root
exp   intrinsic ƒ(x: f64) -> f64;                             // exponential      -- the constant e raised to the power of x
exp2  intrinsic ƒ(x: f64) => pow(2, x)
mod   ƒ(x: f64, y: f64) -> f64;

// Logarithmic functions
log   ƒ(x: f64) -> f64;    // the power to which the constant e has to be raised to produce x.
log2  ƒ(x: f64) -> f64;    
log10 ƒ(x: f64) -> f64;

// Trigonometric functions
sin   intrinsic ƒ(x: f64) -> f64;                                  // sine                    
cos   intrinsic ƒ(x: f64) -> f64;                                  // cosine                  
tan   intrinsic ƒ(x: f64) -> f64;                                  // tangent                 
cot   intrinsic ƒ(x: f64) -> f64;                                  // cotangent
sec   intrinsic ƒ(x: f64) => 1 / cos(x);                           // secant
csc   intrinsic ƒ(x: f64) => 1 / sin(x);                           // cosecant
cotan intrinsic ƒ(x: f64) => 1 / tan(x);                           // co-tangent

// Hyperbolic functions
sinh   ƒ(x: f64) => (exp(x) - exp(-x)) / 2;                        // hyperbolic sine         
cosh   ƒ(x: f64) => (exp(x) + exp(-x)) / 2;                        // hyperbolic cosine       
tanh   ƒ(x: f64) => (exp(x) - exp(-x)) / (exp(x) + exp(-x));       // hyperbolic tangent      
coth   ƒ(x: f64) -> f64;                                           // hyperbolic cotangent   
sech   ƒ(x: f64) => 2 / (exp(x) + exp(-x));                        // hyperbolic secant
csch   ƒ(x: f64) => 2 / (exp(x) - exp(-x));                        // hyperbolic cosecant

asin   ƒ(x: f64) => atan(x / sqrt(-x * x + 1));                    // arc-sine                
acos   ƒ(x: f64) => atan(-x / sqrt(-x * x + 1)) + 2 * Atn(1);      // arc-cosine              
atan   ƒ(x: f64) ->  T;                                            // arc-tangent             
acot   ƒ(x: f64) => (exp(x) + exp(-x)) / (exp(x) - exp(-x));       // arc-cotangent
asec   ƒ(x: f64) => 2 * atan(1) - atan(sign(x) / sqrt(x * x - 1)); // arc-secant
acsc   ƒ(x: f64) => atan(sign(x) / sqrt(x * x - 1));               // arc-cosecant
acotan ƒ(x: f64) => 2 * atan(1) - atan(x); 

asinh  ƒ(x: f64) => log(x + sqrt(x * x + 1));                      // hyperbolic arc-sine     
acosh  ƒ(x: f64) => log(sqrt(x * x - 1) + x);                      // hyperbolic arc-cosine   
atanh  ƒ(x: f64) => log(sqrt(-x * x + 1) / x);                     // hyperbolic arc-tangent  
acoth  ƒ(x: f64) => log((x + 1) / x) + log(x / (x - 1)) / 2;       // hyperbolic arc-cotangent
asech  ƒ(x: f64) => log(sqrt(x * x - 1) + 1);                      // hyperbolic arc-secant   
acsch  ƒ(x: f64) => log(x + sqrt(x * x + 1));                      // hyperbolic arc-cosecant 

atan2  ƒ(x: f64, y: f64) -> f64;                                              

// inversesqrt   // inverse square root (opengl)

// fma // Fused Mutiply Add




// 