namespace D.Imaging
{
    public class ColorModule : Module
    {
        public ColorModule()
        {
            Add("rgb", new RgbConstructor());
        }
    }
}