
using static E.Units.ElectromagismUnits;

namespace E.Units
{
    public class ElectromagnetismUnitSet : UnitSet
    {
        public ElectromagnetismUnitSet()
        {
            Add("Ω",       Ohm);       // Electric resistance
            Add("A",       Ampere);    // Electric current
            Add("Wb",      Weber);     // Magnetic flux            | V·s = kg·m2·s−2·A−1
            Add("T",       Tesla);     // Magnetic flux density    | Wb/m2 = kg·s−2·A−1 = N·A−1·m−1
                           
            Add("W",       Watt);      // Electric power           | V·A = kg·m2·s−3
            Add("F",       Farad);     // Capacitance              | C/V = kg−1·m−2·A2·s4
            Add("H",       Henry);     // Inductance               | Wb/A = V·s/A = kg·m2·s−2·A−2
            Add("V",       Volt);
            Add("C",       Coulomb);   // Electric Charge
            Add("S",       Siemens);

            // Radition
            Add("Bq", Becquerel);
            Add("Sv", Sievert);
            Add("Gy", Gray);

            // Permittivity
            // Conductivity
            // Magnetic flux densicity

            // Light
            Add("lx", Illuminance);
            Add("lm", LuminousFlux);
        }
    }
}