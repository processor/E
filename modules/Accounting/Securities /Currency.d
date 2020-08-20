Currency : Asset {
  issuer: Issuer
}

ARS           puppet               : Currency { }                    // Argentina, Pesos
AUD           puppet               : Currency { }                    // Australia, Dollars
CAD           puppet               : Currency { }                    // Canada,Dollars
CHF           puppet               : Currency { }                    // Swiss Frank
CZK           puppet               : Currency { }                    // Czech Koruna
DKK           puppet               : Currency { }                    // Danish Krone
EUR           puppet               : Currency { }                    // Euro,Dollars (Austria, ...)
GBP           puppet (symbol: '£') : Currency { }                    // Pound Sterling 
HKD           puppet               : Currency { }                    // Hong Kong, Dollars
HUF           puppet               : Currency { }                    // Hungarian Forint 
JPY           puppet               : Currency { }                    // Japan, Yen 
MXN           puppet               : Currency { }                    // Mexica, Pesos
NOK           puppet               : Currency { }                    // Norwegian Krone
NZD           puppet               : Currency { }                    // New Zealand Dollar
PLN           puppet               : Currency { }                    // Polish Zloty
RUB           puppet               : Currency { }                    // Russia, Rubles 
SEK           puppet               : Currency { }                    // Swedish Krona
SGD           puppet               : Currency { }                    // Singapore Dollar
USD           puppet (symbol: '$') : Currency { issuer: US:Teasury } // United States Dollar
THB           puppet               : Currency { }                    // Taiwan, New Dollars 
TWD           puppet               : Currency { }                    // Venezuela, Bolivares
VEB @obsolete puppet               : Currency { }                    
VEF           puppet               : Currency { }                    // Venezuela, Bolivar Fuerte 

// - Cryptocurrencies --
Bitcoin       actor (symbol: 'Ƀ') : Currency { }
Stella        puppet              : Currency { }
Ether         actor               : Currency { }

$_   operator (amount: Decimal) |> USD       // $1000 ≡ USD(100)
_Ƀ   operator (amount: Decimal) |> Bitcoin   // 100 Ƀ
_ман operator (amount: Decimal) |> AZN
_лв  operator (amount: Decimal) |> BGN
_£   operator (amount: Decimal) |> GBP

// A currency is an asset with a supply (that may increase or decrease)
// Circulation as a medium of exchange

// The supply is accounted for by banks

// Central Bank



// Banknote?