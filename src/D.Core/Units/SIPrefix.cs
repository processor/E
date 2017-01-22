using System;
using System.Collections.Generic;

namespace D.Units
{
    using static Math;

    public struct SIPrefix
    {
        public static readonly SIPrefix None = new SIPrefix(null, 1);

        private readonly static double yocto    = Pow(10, -24); // 10^-24
        private readonly static double zepto    = Pow(10, -21); // 10^-21
        private readonly static double atto     = Pow(10, -18); // 10^-18
        private readonly static double femto    = Pow(10, -15); // 10^-15
        private readonly static double pico     = Pow(10, -12); // 10^-12
        private readonly static double nano     = Pow(10, -9);  // 10^-9
        private readonly static double micro    = Pow(10, -6);  // 10^-6
        private readonly static double milli    = Pow(10, -3);  // 10−3
        private readonly static double centi    = Pow(10, -2);  // 10^-2
        private readonly static double deci     = Pow(10, -1);  // 10^-1

        private readonly static double deca     = Pow(10, 1);  // 10^1 
        private readonly static double hecto    = Pow(10, 2);  // 10^2
        private readonly static double kilo     = Pow(10, 3);  // 10^3
        private readonly static double mega     = Pow(10, 6);  // 10^6
        private readonly static double giga     = Pow(10, 9);  // 10^9
        private readonly static double tera     = Pow(10, 12); // 10^12
        private readonly static double peta     = Pow(10, 15); // 10^15
        private readonly static double exa      = Pow(10, 18); // 10^18
        private readonly static double zetta    = Pow(10, 21); // 10^21
        private readonly static double yotta    = Pow(10, 24); // 10^24
           
        public static readonly SIPrefix y =  new SIPrefix("y" ,  yocto); // 10^-24
        public static readonly SIPrefix z =  new SIPrefix("z" ,  zepto); // 10^-21
        public static readonly SIPrefix a =  new SIPrefix("a" ,  atto ); // 10^-18
        public static readonly SIPrefix f =  new SIPrefix("f" ,  femto); // 10^-15
        public static readonly SIPrefix p =  new SIPrefix("p" ,  pico ); // 10^-12
        public static readonly SIPrefix n =  new SIPrefix("n" ,  nano ); // 10^-9
        public static readonly SIPrefix µ =  new SIPrefix("µ",   micro); // 10^-6
        public static readonly SIPrefix m =  new SIPrefix("m" ,  milli); // 10−3
        public static readonly SIPrefix c =  new SIPrefix("c" ,  centi); // 10^-2
        public static readonly SIPrefix d =  new SIPrefix("d" ,  deci ); // 10^-1
                                                          
        public static readonly SIPrefix da = new SIPrefix("da",  deca ); // 10^1 
        public static readonly SIPrefix h =  new SIPrefix("h" ,  hecto); // 10^2
        public static readonly SIPrefix k =  new SIPrefix("k" ,  kilo ); // 10^3
        public static readonly SIPrefix M =  new SIPrefix("M" ,  mega ); // 10^6
        public static readonly SIPrefix G =  new SIPrefix("G" ,  giga ); // 10^9
        public static readonly SIPrefix T =  new SIPrefix("T" ,  tera ); // 10^12
        public static readonly SIPrefix P =  new SIPrefix("P" ,  peta ); // 10^15
        public static readonly SIPrefix E =  new SIPrefix("E" ,  exa  ); // 10^18
        public static readonly SIPrefix Z =  new SIPrefix("Z" ,  zetta); // 10^21
        public static readonly SIPrefix Y =  new SIPrefix("Y" ,  yotta); // 10^24

       

        private static Dictionary<string, double> scales = new Dictionary<string, double> {
            { "yocto",  yocto },
            { "zepto",  zepto },
            { "atto",   atto  },
            { "femto",  femto },
            { "pico",   pico  },
            { "nano",   nano  },
            { "micro",  micro },
            { "milli",  milli },
            { "centi",  centi },
            { "deci",   deci  },
                          
            { "deca",   deca  },
            { "hecto",  hecto },
            { "kilo",   kilo  },
            { "mega",   mega  },
            { "giga",   giga  },
            { "tera",   tera  },
            { "peta",   peta  },
            { "exa",    exa   },
            { "zetta",  zetta },
            { "yotta",  yotta },
        };

        private SIPrefix(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public double Value { get; }

        public int Length => Name.Length;

        public static SIPrefix Parse(string text)
        {
            SIPrefix prefix;

            if (text.Length <= 2)
            {
                TryParseSymbol(text, out prefix);
            }
            else
            {
                TryParseName(text, out prefix);
            }

            return prefix;
        }

        public static bool TryParseSymbol(string text, out SIPrefix prefix)
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
                case 'd': prefix = (text[1] == 'a') ? da : d; break;
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
                    prefix = default(SIPrefix);

                    return false;
            }

            return true;
        }

        public static bool TryParseName(string name, out SIPrefix prefix)
        {
            double val;

            if (scales.TryGetValue(name, out val))
            {
                prefix = new SIPrefix(name, val);

                return true;
            }

            prefix = default(SIPrefix);

            return false;
        }

        // Scientific Notiation
    }
}