namespace D
{
    public class Script
    {
        public static IObject Evaluate(string text)
            => Evaluate(text, new Env());

        public static IObject Evaluate(string text, Env env)
        {
            var evaulator = new Evaulator(env);

            evaulator.Evaluate(text);

            return evaulator.This;
        }
    }
}