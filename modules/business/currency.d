Money type { 
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
, VEB obsolete, 
, VEF                       // Venezuela, Bolivar Fuerte 

// -- Cryptocurrencies --
, Bitcoin
, Stella,
, Ether,

: Money;

$   prefix  operator (amount: Decimal) |> USD      // $1000 ≡ USD(100)
Ƀ   postfix operator (amount: Decimal) |> Bitcoin  // 100Ƀ                 ref: https://bitcoinmagazine.com/articles/bitcoin-finally-gets-an-approved-unicode-character-point-1446580490
؋   postfix operator (amount: Decimal) |> AFN
ман postfix operator (amount: Decimal) |> AZN
лв  postfix operator (amount: Decimal) |> BGN
£   postfix operator (amount: Decimal) |> GBP
