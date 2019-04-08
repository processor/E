namespace D.Imaging
{
    public class ColorModule : Module
    {
        public ColorModule()
        {
            Add("rgb", new RgbConstructor());
            Add("transparent", new Rgba(0, 0, 0, 0));
        }
    }
}