namespace D
{
    public class Script
    {
        public static IObject Evaluate(string text)
        {
            return Evaluate(text, new Node());
        }

        public static IObject Evaluate(string text, Node env)
        {
            var evaulator = new Evaulator(env);

            evaulator.Evaluate(text);

            return evaulator.This;
        }
    }
}