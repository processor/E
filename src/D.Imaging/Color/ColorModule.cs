using Carbon.Color;

namespace D.Imaging
{
    public class ColorModule : Module
    {
        public ColorModule()
        {
            AddExport("rgb", new RgbConstructor());
            AddExport("transparent", NamedColor.Transparent);
            // Add("adjust", new AdjustColorFunction());
        }
    }
}