
using static D.Units.UnitInfo;

namespace D.Units
{
    public sealed class GeneralUnitSet : UnitSet
    {
        public GeneralUnitSet()
        {
            // TODO: Primary Units 

            Add("m",    Meter);  // Length
            Add("s",    Second); // Time
            Add("g",    Gram);   // Mass

            Add("Pa",   Pascal);
            Add("sr",   Steradian); // Square radian (㏛)
           
            // Add("J",     Joule);
            // Add("N",     Newton);

            // Add("gray",  Gray);
            Add("mole",  Mole);
            Add("katal", Katal);

            // Angles (Plane)
            Add("rad",    Radian);
            Add("grad",   Gradian);
            Add("deg",    Degree);
            Add("turn",   Turn);

            // Frequency
            Add("Hz",    Hertz);

            Add("mol", Mole);

            // Time
            Add("min", Minute);
            Add("h",   Hour);
            Add("wk",  Week);

            Add("cd", Candela);

            Add("kat", Katal);

            // Volume
            Add("L", Liter);
            
            // Non-SI units
            Add("lb", Pound);

            // Dimensionless
            Add("%", Percentage);
        }
    }
}