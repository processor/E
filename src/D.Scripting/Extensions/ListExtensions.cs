using System.Collections.Generic;

namespace D.Parsing
{
    internal static class ListExtensions
    {
        public static T[] Extract<T>(this List<T> list)
        {
            var array = list.ToArray();

            list.Clear();

            return array;
        }
    }
}