Depreciation event {
  asset  : Asset
  amount : Decimal
}

Depreciation `Schedule record {
  interval : Interval				
  callback : (asset: Asset) -> Depreciation // percentage: 10%, fixed: $100
}