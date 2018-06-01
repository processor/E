Product record {
  manufacturer? :   Entity
  name          :   String
  identifiers   : [ Product `Identifier  ]
}

// Brand (Make) | Ford, ...
// Model        | Focus

Product`Flags enum {
  Durable    = 1 << 0,
  Tangible   = 1 << 1,
  Intangible = 1 << 2
}


// tangible   (physical good)
// intangible (services, insurance policy, etc)

// https://en.wikipedia.org/wiki/Product_(business)
// In marketing, a product is anything that can be offered to a market that might satisfy a want or need.
// In retailing, products are called merchandise. 
// In manufacturing, products are bought as raw materials and sold as finished goods. 
// A service is another common product type.
