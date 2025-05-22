namespace E.Units;

public enum UnitType
{
    Mole    = 41509,
    Pascal  = 44395,
    Litre   = 11582,   
    Katal   = 208634,
    Kelvin  = 11579,
    Celsius = 25267,
    Meter   = 11573,
    Pica    = 2718629,

    Millilitre = 2332346,

    // Length 
    Millimeter = 174789,
    Centimeter = 174728,
    Kilometre  = 828224,

    // CSS
    QuaterMillimeters = 131966422,
    Px = 355198,
    Dpi = 305896,
    Dpcm = 5299480,
    Pt = 156389,
    Dppx = 110742003, // Dots per pixel
    Vmin = 125516432,
    Em   = 1334525,
    Fr   = 131987530,

    ångström = 81454,
    AstronomicalUnit = 1811,
    Parsec = 12129,
    
    // Imperial
    Inch = 218593,
    Foot = 3710,
    Yard = 482798,

    // Time
    Nanosecond  = 838801,
    Millisecond = 723733,
    Second      = 11574,
    Minute      = 7727,
    Hour        = 25235,
    Day         = 573,
    Week        = 23387, 
    Year        = 577,

    Candela = 83216,
    Hz      = 39369,
    kHz     = 2143992,

    // Area
    Acre            = 81292,    // ACR  | 
    SquareFoot      = 857027,   // FTK  | ft²
    SquareMeter     = 25343,    // MTK  | m²
    SquareMile      = 232291,
    SquareKilometer = 712226,
    SquareInch      = 1063786,

    // Data transfer
    Bit = 8805,
    Baud = 192027,
    Radian = 33680,


    // Mass
    Gram = 41_803,
    Kilogram = 11570,

    Ounce = 48013,
    Pound = 100995,


    // Angle
    Steradian = 177612,
    Degree = 28390,
    Gradian = 208528,
    Turn = 304479,

    // Data
    Byte     = 8799,
    Kibibyte = 79756,
    Mebibyte = 79758,
    Tebibyte = 79769,

    Kilobyte = 79726,
    Megabyte = 79735,
    Gigabyte = 79738,

    Percent = 11229,


    Newton = 12438,

    Ampere       = 25272,
    Coulomb      = 25406,
    Ohm          = 47083,
    Farad        = 131255,
    Henry        = 163354,
    Joule        = 25269,
    Siemens      = 169893,
    Tesla        = 163343,
    Volt         = 25250,
    Watt         = 25236,
    Weber        = 170804,   
    Sievert      = 103246,
    Gray         = 190095,
    Becquerel    = 102573,    
    Illuminance  = 194411,
    LuminousFlux = 107780,

    // Units of Volume

    MetricTeaspoon   = 88296091,
    MetricTablespoon = 105816197,
    MetricCup        = 64866333,

    US_LegalCup     = 105816270, // 240 ml

    US_FluidDram    = 105806592, // fl dr 60 min	
    US_Teaspoon     = 105816142, // tsp
    US_Tablespoon   = 105816198, // tbsp
    US_FluidOunce   = 32750759,

    // US_Shot      = 0, // jig
    US_Gill         = 93678895,  // gill | 4 fluid ounce
    US_LiquidCup    = 105816269, // cp
    US_LiquidPint   = 32750621,  // 2 cp	
    US_LiquidQuart  = 98793408,  // 2 pt	
    US_LiquidGallon = 23925413,  // 4 qt	
    // US_Barrel     = 0,  // 31.5 gal    


    CubicMetre = 25517
}

// https://unece.org/sites/default/files/2023-10/rec20_rev3_Annex3e.pdf