Currency : Asset { 

}

ARS           actor               : Currency { } // Argentina, Pesos
AUD           actor               : Currency { } // Australia, Dollars
CAD           actor               : Currency { } // Canada,Dollars
CHF           actor               : Currency { } // Swiss Frank
CZK           actor               : Currency { } // Czech Koruna
DKK           actor               : Currency { } // Danish Krone
EUR           actor               : Currency { } // Euro,Dollars (Austria, ...)
GBP           actor (symbol: '£') : Currency { } // Pound Sterling 
HKD           actor               : Currency { } // Hong Kong, Dollars
HUF           actor               : Currency { } // Hungarian Forint 
JPY           actor               : Currency { } // Japan, Yen 
MXN           actor               : Currency { } // Mexica, Pesos
NOK           actor               : Currency { } // Norwegian Krone
NZD           actor               : Currency { } // New Zealand Dollar
PLN           actor               : Currency { } // Polish Zloty
RUB           actor               : Currency { } // Russia, Rubles 
SEK           actor               : Currency { } // Swedish Krona
SGD           actor               : Currency { } // Singapore Dollar
USD           actor (symbol: '$') : Currency { } // United States Dollar
THB           actor               : Currency { } // Taiwan, New Dollars 
TWD           actor               : Currency { } // Venezuela, Bolivares
VEB @obsolete actor               : Currency { }
VEF           actor               : Currency { } // Venezuela, Bolivar Fuerte 

// - Cryptocurrencies --
Bitcoin       actor (symbol: 'Ƀ') : Currency { }
Stella        actor               : Currency { }
Ether         actor               : Currency { }

$_   operator (amount: Decimal) |> USD       // $1000 ≡ USD(100)
_Ƀ   operator (amount: Decimal) |> Bitcoin   // 100 Ƀ
_ман operator (amount: Decimal) |> AZN
_лв  operator (amount: Decimal) |> BGN
_£   operator (amount: Decimal) |> GBP


// A currency is an asset with a supply (that may increase or decrease)
// The supply is accounted for by banks