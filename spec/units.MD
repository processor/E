# Units

### Declarations

```
Pound unit : Weight {
  symbol: "lb",
  value: 0.453592 kg
}
```

### Literals

```
5 lb
```

### Conversions

If a conversion can made, it will automatically be applied when provided to a call site or object constructor.

```
Package {
  weight: Number<kg>
}

Package { weight: 5lb } # Package(2.26796)
```
