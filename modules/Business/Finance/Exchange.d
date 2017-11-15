Exchange protocol { 
  offer (
    type     : Buy | Sell,
    asset    : Asset,
    quantity : Decimal,
    currency : Currency,
    price    : Decimal
  ) -> Offer

  offers -> [ Offer ]
  assets -> [ Assets ]
}

NASDAQ                  class : Exchange
New`York`Stock Exchange class : Exchange
