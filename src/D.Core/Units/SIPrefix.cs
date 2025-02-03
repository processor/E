using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace E.Units;

public readonly struct MetricPrefix : IEquatable<MetricPrefix>
{
    public static readonly MetricPrefix None = new (null!, 1);

    private const double yocto = 1e-24; // 10^-24
    private const double zepto = 1e-21; // 10^-21
    private const double atto  = 1e-18; // 10^-18
    private const double femto = 1e-15; // 10^-15
    private const double pico  = 1e-12; // 10^-12
    private const double nano  = 1e-9;  // 10^-9
    private const double micro = 1e-6;  // 10^-6
    private const double milli = 1e-3;  // 10^-3
    private const double centi = 1e-2;  // 10^-2
    private const double deci  = 1e-1;  // 10^-1

    private const double deca  = 1e1;   // 10^1 
    private const double hecto = 1e2;   // 10^2
    private const double kilo  = 1e3;   // 10^3
    private const double mega  = 1e6;   // 10^6
    private const double giga  = 1e9;   // 10^9
    private const double tera  = 1e12;  // 10^12
    private const double peta  = 1e15;  // 10^15
    private const double exa   = 1e18;  // 10^18
    private const double zetta = 1e21;  // 10^21
    private const double yotta = 1e24;  // 10^24

    public static readonly MetricPrefix y =  new ("y" ,  yocto); // 10^-24
    public static readonly MetricPrefix z =  new ("z" ,  zepto); // 10^-21
    public static readonly MetricPrefix a =  new ("a" ,  atto ); // 10^-18
    public static readonly MetricPrefix f =  new ("f" ,  femto); // 10^-15
    public static readonly MetricPrefix p =  new ("p" ,  pico ); // 10^-12
    public static readonly MetricPrefix n =  new ("n" ,  nano ); // 10^-9
    public static readonly MetricPrefix µ =  new ("µ",   micro); // 10^-6
    public static readonly MetricPrefix m =  new ("m" ,  milli); // 10−3
    public static readonly MetricPrefix c =  new ("c" ,  centi); // 10^-2
    public static readonly MetricPrefix d =  new ("d" ,  deci ); // 10^-1
                                                  
    public static readonly MetricPrefix da = new ("da",  deca ); // 10^1 
    public static readonly MetricPrefix h =  new ("h" ,  hecto); // 10^2
    public static readonly MetricPrefix k =  new ("k" ,  kilo ); // 10^3
    public static readonly MetricPrefix M =  new ("M" ,  mega ); // 10^6
    public static readonly MetricPrefix G =  new ("G" ,  giga ); // 10^9
    public static readonly MetricPrefix T =  new ("T" ,  tera ); // 10^12
    public static readonly MetricPrefix P =  new ("P" ,  peta ); // 10^15
    public static readonly MetricPrefix E =  new ("E" ,  exa  ); // 10^18
    public static readonly MetricPrefix Z =  new ("Z" ,  zetta); // 10^21
    public static readonly MetricPrefix Y =  new ("Y" ,  yotta); // 10^24

    private static readonly FrozenDictionary<string, double> s_scales = new KeyValuePair<string, double>[] {
        new("yocto",  yocto ),
        new("zepto",  zepto ),
        new("atto",   atto  ),
        new("femto",  femto ),
        new("pico",   pico  ),
        new("nano",   nano  ),
        new("micro",  micro ),
        new("milli",  milli ),
        new("centi",  centi ),
        new("deci",   deci  ),

        new("deca",   deca  ),
        new("hecto",  hecto ),
        new("kilo",   kilo  ),
        new("mega",   mega  ),
        new("giga",   giga  ),
        new("tera",   tera  ),
        new("peta",   peta  ),
        new("exa",    exa   ),
        new("zetta",  zetta ),
        new("yotta",  yotta ),
    }.ToFrozenDictionary();
    
    private MetricPrefix(string name, double value)
    {
        Name = name;
        Value = value;
    }

    public readonly string Name { get; }

    public readonly double Value { get; }

    public readonly int Length => Name.Length;

    public static MetricPrefix Parse(string text)
    {
        MetricPrefix prefix;

        if (text.Length <= 2)
        {
            _ = TryParseSymbol(text, out prefix);
        }
        else
        {
            _ = TryParseName(text, out prefix);
        }

        return prefix;
    }

    public static bool TryParseSymbol(ReadOnlySpan<char> text, out MetricPrefix prefix)
    {
        switch (text[0])
        {
            case 'y': prefix = y; break;
            case 'z': prefix = z; break;
            case 'a': prefix = a; break;
            case 'f': prefix = f; break;
            case 'p': prefix = p; break;
            case 'n': prefix = n; break;
            case 'µ': prefix = µ; break;
            case 'm': prefix = m; break;
            case 'c': prefix = c; break;
            case 'd': prefix = text[1] == 'a' ? da : d; break;
            case 'h': prefix = h; break;
            case 'k': prefix = k; break;
            case 'M': prefix = M; break;
            case 'G': prefix = G; break;
            case 'T': prefix = T; break;
            case 'P': prefix = P; break;
            case 'E': prefix = E; break;
            case 'Z': prefix = Z; break;
            case 'Y': prefix = Y; break;

            default:
                prefix = default;

                return false;
        }

        return true;
    }

    public static bool TryParseName(string name, out MetricPrefix prefix)
    {
        if (s_scales.TryGetValue(name, out double val))
        {
            prefix = new MetricPrefix(name, val);

            return true;
        }

        prefix = default;

        return false;
    }

    public static bool TryGetFromScale(double scale, out MetricPrefix prefix)
    {
        prefix = scale switch {
            yocto => y,  // 10^-24
            zepto => z,  // 10^-21
            atto  => a,  // 10^-18
            femto => f,  // 10^-15
            pico  => p,  // 10^-12
            nano  => n,  // 10^-9
            micro => µ,  // 10^-6
            milli => m,  // 10^-3
            centi => c,  // 10^-2
            deci  => d,  // 10^-1
            deca  => da,  // 10^1 
            hecto => h,   // 10^2
            kilo  => k,   // 10^3
            mega  => M,   // 10^6
            giga  => G,   // 10^9
            tera  => T,   // 10^12
            peta  => P,   // 10^15
            exa   => E,   // 10^18
            zetta => Z,   // 10^21
            yotta => Y,   // 10^24
            _ => default
        };
        
        return prefix.Value != 0;
    }

    public bool Equals(MetricPrefix other)
    {
        return string.Equals(Name, other.Name, StringComparison.Ordinal) 
            && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is MetricPrefix other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Value);
    }
}