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

: Money type {
  amount: Decimal 
}

$   prefix  => USD      // $1000 ≡ USD(100)
Ƀ   postfix => Bitcoin  // 100Ƀ                 ref: https://bitcoinmagazine.com/articles/bitcoin-finally-gets-an-approved-unicode-character-point-1446580490
؋   postfix => AFN
ман postfix => AZN
лв  postfix => BGN
£   postfix => GBP
