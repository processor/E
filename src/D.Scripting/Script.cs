namespace E
{
    public class Script
    {
        public static object Evaluate(string text)
        {
            return Evaluate(text, new Node());
        }

        public static object Evaluate(string text, Node env)
        {
            var evaulator = new Evaluator(env);

            evaulator.Evaluate(text);

            return evaulator.This!;
        }
    }
}