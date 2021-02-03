using System.Collections.Generic;

namespace E.Units
{
    public class UnitSet
    {
        public static readonly UnitSet Default = new UnitSet(
            new GeneralUnitSet(),
            new ThermodynamicUnitSet(),
            new ElectromagismUnitSet(),
            new MechanicalUnitSet(),
            new CssUnitSet()
        );

        private readonly Dictionary<string, UnitInfo> items = new ();

        public UnitSet() { }

        public UnitSet(params UnitSet[] collections)
        {
            foreach (var set in collections)
            {
                AddRange(set);
            }
        }

        public bool Contains(string name) => items.ContainsKey(name);

        public bool TryGet(string name, out UnitInfo type) => items.TryGetValue(name, out type);

        public void Add(string name, UnitInfo type)
        {
            items[name] = type;
        }

        public void Add(UnitInfo type)
        {
            items[type.Name] = type;
        }

        public void AddRange(UnitSet collection)
        {
            foreach (var item in collection.items)
            {
                items[item.Key] = item.Value;
            }
        }
    }
}