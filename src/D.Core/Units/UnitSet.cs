using System.Collections.Generic;

namespace E.Units;

public class UnitSet : Dictionary<string, UnitInfo>
{
    public void Add(UnitInfo type)
    {
        this[type.Name] = type;
    }
}