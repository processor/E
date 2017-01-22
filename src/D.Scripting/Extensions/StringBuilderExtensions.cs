using System.Text;

namespace D.Parsing
{
    internal static class StringBuilderExtensions
    {
        public static string Extract(this StringBuilder sb)
        {
            var text = sb.ToString();

            sb.Clear();

            return text;
        }
    }
}
