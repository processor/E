abs      ƒ (x: Float)   -> Float;                   // Absolute value
abs      ƒ (x: Integer) -> Integer;                   

// Rounding
round    ƒ(x: Float)                           -> Float;
round    ƒ(x: Float, digits: Integer)          -> Float;
round    ƒ(x: Float, digits: Integer, Midway)  -> Float;
ceiling  ƒ(x: Float) -> Float;                                                    // ceil in JS
floor    ƒ(x: Float) -> Float;
truncate ƒ(x: Float) -> Float;

// Clamping
clamp ƒ(x: Float  , min: Float  , max: Float)   -> Float;
clamp ƒ(x: Integer, min: Integer, max: Integer) -> Integer;

// Signs
sign ƒ(x: Float)   -> Float;       // 1 when positive, 0 when 0, - 1 when negitive
sign ƒ(x: Integer) -> Integer;

// Min / max
min  ƒ(x: Float,   y: Float)   -> Float;
min  ƒ(x: Integer, y: Integer) -> Integer;
max  ƒ(x: Float,   y: Float)   -> Float;
max  ƒ(x: Integer, y: Integer) -> Integer;

// Logarithmic functions
log    ƒ(x) -> Float;    // the power to which the constant e has to be raised to produce x.
log2   ƒ(x) -> Float;    
log10  ƒ(x) -> Float;

// Exponentiation functions
pow    ƒ(x: Float, exponent: Float) -> Float;             // exponentiation
sqrt   ƒ(x: Float) -> Float;                              // square root
cbrt   ƒ(x: Float) -> Float;                              // cube root
exp    ƒ(x: Float) -> Float;                              // exponential      -- the constant e raised to the power of x.
exp2   ƒ(x: Float) => pow(2, x)

mod    ƒ(x: Float, y: Float) -> Float;
                                                                          
// Trigonometric functions
sin       ƒ(x: Float) -> Float;                                        // sine                    
cos       ƒ(x: Float) -> Float;                                        // cosine                  
tan       ƒ(x: Float) -> Float;                                        // tangent                 
cot       ƒ(x: Float) -> Float;                                        // cotangent
sec       ƒ(x: Float) => 1 / cos(x);                                   // secant
csc       ƒ(x: Float) => 1 / sin(x);                                   // cosecant
cotan     ƒ(x: Float) => 1 / tan(x);                                   // co-tangent

// Hyperbolic functions
sinh   ƒ(x: Float) => (exp(x) - exp(-x)) / 2;                          // hyperbolic sine         
cosh   ƒ(x: Float) => (exp(x) + exp(-x)) / 2;                          // hyperbolic cosine       
tanh   ƒ(x: Float) => (exp(x) - exp(-x)) / (exp(x) + exp(-x));         // hyperbolic tangent      
coth   ƒ(x: Float) -> Float;                                           // hyperbolic cotangent   
sech   ƒ(x: Float) => 2 / (exp(x) + exp(-x));                          // hyperbolic secant
csch   ƒ(x: Float) => 2 / (exp(x) - exp(-x));                          // hyperbolic cosecant

asin   ƒ(x: Float) => atan(x / sqrt(-x * x + 1));                      // arc-sine                
acos   ƒ(x: Float) => atan(-x / sqrt(-x * x + 1)) + 2 * Atn(1);        // arc-cosine              
atan   ƒ(x: Float) ->  T;                                              // arc-tangent             
acot   ƒ(x: Float) => (exp(x) + exp(-x)) / (exp(x) - exp(-x));         // arc-cotangent
asec   ƒ(x: Float) => 2 * atan(1) - atan(sign(x) / sqrt(x * x - 1));   // arc-secant
acsc   ƒ(x: Float) => atan(sign(x) / sqrt(x * x - 1));                 // arc-cosecant
acotan ƒ(x: Float) => 2 * atan(1) - atan(x); 

asinh  ƒ(x: Float) => log(x + sqrt(x * x + 1));                        // hyperbolic arc-sine     
acosh  ƒ(x: Float) => log(sqrt(x * x - 1) + x);                        // hyperbolic arc-cosine   
atanh  ƒ(x: Float) => log(sqrt(-x * x + 1) / x);                       // hyperbolic arc-tangent  
acoth  ƒ(x: Float) => log((x + 1) / x) + log(x / (x - 1)) / 2;         // hyperbolic arc-cotangent
asech  ƒ(x: Float) => log(sqrt(x * x - 1) + 1);                        // hyperbolic arc-secant   
acsch  ƒ(x: Float) => log(x + sqrt(x * x + 1));                        // hyperbolic arc-cosecant 

atan2  ƒ(x: Float, y: Float) -> Float;                                              


// inversesqrt   // inverse square root (opengl)




// fma // Fused Mutiply Add
