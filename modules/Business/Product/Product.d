Product record {
  manufacturer? :   Entity
  name          :   String
  identifiers   : [] UID

  // Flags -
  is tangible   : Boolean
  is durable    : Boolean
}

Taxable impl for Product {

}


// Examples

// isbn   : 0-486-27557-4
// upc    : 0424324234234
// amazon : products / 123

// is Durable


// Brand (Make) | Ford, ...
// Model        | Focus


// tangible   (physical good)
// intangible (services, insurance policy, etc)

// https://en.wikipedia.org/wiki/Product_(business)
// In marketing, a product is anything that can be offered toå a market that might satisfy a want or need.
// In retailing, products are called merchandise. 
// In manufacturing, products are bought as raw materials and sold as finished goods. 
// A service is another common product type.
