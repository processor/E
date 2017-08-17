Currency struct { 
  amount: Decimal
}

  ARS                       // Argentina, Pesos
, AUD                       // Australia, Dollars
, CAD                       // Canada,Dollars
, CHF                       // Swiss Frank
, CZK                       // Czech Koruna
, DKK                       // Danish Krone
, EUR                       // Euro,Dollars (Austria, ...)
, GBP                       // Pound Sterling 
, HKD                       // Hong Kong, Dollars
, HUF                       // Hungarian Forint 
, JPY                       // Japan, Yen 
, MXN                       // Mexica, Pesos
, NOK                       // Norwegian Krone
, NZD                       // New Zealand Dollar
, PLN                       // Polish Zloty
, RUB                       // Russia, Rubles 
, SEK                       // Swedish Krona
, SGD                       // Singapore Dollar
, USD                       // United States Dollar
, THB                       // Taiwan, New Dollars 
, TWD                       // Venezuela, Bolivares
, VEB @obsolete, 
, VEF                       // Venezuela, Bolivar Fuerte 

// -- Cryptocurrencies --
, Bitcoin
, Stella,
, Ether
: Currency

$ _   operator (amount: Decimal) |> USD       // $1000 ≡ USD(100)
_ Ƀ   operator (amount: Decimal) |> Bitcoin   // 100 Ƀ
_ ман operator (amount: Decimal) |> AZN
_ лв  operator (amount: Decimal) |> BGN
_ £   operator (amount: Decimal) |> GBP
