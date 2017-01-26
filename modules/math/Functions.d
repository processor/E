abs      ƒ (x: Number)   -> Number;                   // Absolute value
abs      ƒ (x: Integer) -> Integer;                   

// Rounding
round    ƒ(x: Number)                           -> Number;
round    ƒ(x: Number, digits: Integer)          -> Number;
round    ƒ(x: Number, digits: Integer, Midway)  -> Number;
ceiling  ƒ(x: Number) -> Number;                                                    // ceil in JS
floor    ƒ(x: Number) -> Number;
truncate ƒ(x: Number) -> Number;

// Clamping
clamp ƒ(x: Number  , min: Number  , max: Number)   -> Number;
clamp ƒ(x: Integer, min: Integer, max: Integer) -> Integer;

// Signs
sign ƒ(x: Number)  -> Number;       // 1 when positive, 0 when 0, - 1 when negitive
sign ƒ(x: Integer) -> Integer;

// Min / max
min  ƒ(x: Number  , y: Number) -> Number;
min  ƒ(x: Integer , y: Integer) -> Integer;
max  ƒ(x: Number  , y: Number) -> Number;
max  ƒ(x: Integer , y: Integer) -> Integer;

// Logarithmic functions
log    ƒ(x) -> Number;    // the power to which the constant e has to be raised to produce x.
log2   ƒ(x) -> Number;    
log10  ƒ(x) -> Number;

// Exponentiation functions
pow    ƒ(x: Number, exponent: Number) -> Number;             // exponentiation
sqrt   ƒ(x: Number) -> Number;                              // square root
cbrt   ƒ(x: Number) -> Number;                              // cube root
exp    ƒ(x: Number) -> Number;                              // exponential      -- the constant e raised to the power of x.
exp2   ƒ(x: Number) => pow(2, x)

mod    ƒ(x: Number, y: Number) -> Number;
                                                                          
// Trigonometric functions
sin       ƒ(x: Number) -> Number;                                       // sine                    
cos       ƒ(x: Number) -> Number;                                       // cosine                  
tan       ƒ(x: Number) -> Number;                                       // tangent                 
cot       ƒ(x: Number) -> Number;                                       // cotangent
sec       ƒ(x: Number) => 1 / cos(x);                                   // secant
csc       ƒ(x: Number) => 1 / sin(x);                                   // cosecant
cotan     ƒ(x: Number) => 1 / tan(x);                                   // co-tangent

// Hyperbolic functions
sinh   ƒ(x: Number) => (exp(x) - exp(-x)) / 2;                          // hyperbolic sine         
cosh   ƒ(x: Number) => (exp(x) + exp(-x)) / 2;                          // hyperbolic cosine       
tanh   ƒ(x: Number) => (exp(x) - exp(-x)) / (exp(x) + exp(-x));         // hyperbolic tangent      
coth   ƒ(x: Number) -> Number;                                          // hyperbolic cotangent   
sech   ƒ(x: Number) => 2 / (exp(x) + exp(-x));                          // hyperbolic secant
csch   ƒ(x: Number) => 2 / (exp(x) - exp(-x));                          // hyperbolic cosecant

asin   ƒ(x: Number) => atan(x / sqrt(-x * x + 1));                      // arc-sine                
acos   ƒ(x: Number) => atan(-x / sqrt(-x * x + 1)) + 2 * Atn(1);        // arc-cosine              
atan   ƒ(x: Number) ->  T;                                              // arc-tangent             
acot   ƒ(x: Number) => (exp(x) + exp(-x)) / (exp(x) - exp(-x));         // arc-cotangent
asec   ƒ(x: Number) => 2 * atan(1) - atan(sign(x) / sqrt(x * x - 1));   // arc-secant
acsc   ƒ(x: Number) => atan(sign(x) / sqrt(x * x - 1));                 // arc-cosecant
acotan ƒ(x: Number) => 2 * atan(1) - atan(x); 

asinh  ƒ(x: Number) => log(x + sqrt(x * x + 1));                        // hyperbolic arc-sine     
acosh  ƒ(x: Number) => log(sqrt(x * x - 1) + x);                        // hyperbolic arc-cosine   
atanh  ƒ(x: Number) => log(sqrt(-x * x + 1) / x);                       // hyperbolic arc-tangent  
acoth  ƒ(x: Number) => log((x + 1) / x) + log(x / (x - 1)) / 2;         // hyperbolic arc-cotangent
asech  ƒ(x: Number) => log(sqrt(x * x - 1) + 1);                        // hyperbolic arc-secant   
acsch  ƒ(x: Number) => log(x + sqrt(x * x + 1));                        // hyperbolic arc-cosecant 

atan2  ƒ(x: Number, y: Number) -> Number;                                              


// inversesqrt   // inverse square root (opengl)




// fma // Fused Mutiply Add
