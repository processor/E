Depreciation event {
  asset  : Asset
  amount : Decimal
}

Depreciation`Schedule record {
  interval : Interval				
  run      : (asset: Asset) -> Depreciation // percentage: 10%, fixed: $100
}