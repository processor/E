namespace E.Units;

using static UnitInfo;

public sealed class GeneralUnitSet : UnitSet
{
    // NOTE: Joule is apart of Electromagnetism Units
    // NOTE: Newton is apart of Force Units

    public GeneralUnitSet()
    {
        // TODO: Primary Units 

        Add("m",     LengthUnits.Meter);  // Length
        Add("s",     TimeUnits.Second);   // Time
        Add("g",     Gram);   // Mass
                         
        Add("Pa",    Pascal);
        Add("sr",    Steradian); // Square radian (㏛)

        // Add("gray",  Gray);
        Add("mole",  Mole);
        Add("katal", Katal);

        // Angles (Plane)
        Add("rad",   Radian);
        Add("grad",  Gradian);
        Add("deg",   Degree);
        Add("turn",  Turn);

        // Length
        Add("km", LengthUnits.Kilometre);
        Add("ft", LengthUnits.Foot);
        // Frequency
        Add("Hz",    Hertz);

        Add("mol", Mole);

        // Time
        Add("min", TimeUnits.Minute);
        Add("h",   TimeUnits.Hour);
        Add("wk",  TimeUnits.Week);

        Add("cd", Candela);

        Add("kat", Katal);

        // Volume
        Add("L",  VolumeUnits.Litre);
        Add("ml", VolumeUnits.Millilitre);

        // Non-SI units
        Add("lb", MassUnits.Pound);

        // Dimensionless
        Add("%", Percent);
    }
}