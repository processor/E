Currency : Asset { 

}

ARS           actor : Currency { }                    // Argentina, Pesos
AUD           actor : Currency { }                    // Australia, Dollars
CAD           actor : Currency { }                    // Canada,Dollars
CHF           actor : Currency { }                    // Swiss Frank
CZK           actor : Currency { }                    // Czech Koruna
DKK           actor : Currency { }                    // Danish Krone
EUR           actor : Currency { }                    // Euro,Dollars (Austria, ...)
GBP           actor : Currency { let symbol = '£' }   // Pound Sterling 
HKD           actor : Currency { }                    // Hong Kong, Dollars
HUF           actor : Currency { }                    // Hungarian Forint 
JPY           actor : Currency { }                    // Japan, Yen 
MXN           actor : Currency { }                    // Mexica, Pesos
NOK           actor : Currency { }                    // Norwegian Krone
NZD           actor : Currency { }                    // New Zealand Dollar
PLN           actor : Currency { }                    // Polish Zloty
RUB           actor : Currency { }                    // Russia, Rubles 
SEK           actor : Currency { }                    // Swedish Krona
SGD           actor : Currency { }                    // Singapore Dollar
USD           actor : Currency { let symbol = '$' }   // United States Dollar
THB           actor : Currency { }                    // Taiwan, New Dollars 
TWD           actor : Currency { }                    // Venezuela, Bolivares
VEB @obsolete actor : Currency { }
VEF           actor : Currency { }                    // Venezuela, Bolivar Fuerte 

// -- Cryptocurrencies --
Bitcoin       actor : Currency { let symbol = 'Ƀ' }
Stella        actor : Currency { }
Ether         actor : Currency { }

// A currency is an asset with a supply

$ _   operator (amount: Decimal) |> USD       // $1000 ≡ USD(100)
_ Ƀ   operator (amount: Decimal) |> Bitcoin   // 100 Ƀ
_ ман operator (amount: Decimal) |> AZN
_ лв  operator (amount: Decimal) |> BGN
_ £   operator (amount: Decimal) |> GBP
