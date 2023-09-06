namespace E;

public class Script
{
    public static object Evaluate(string text)
    {
        return Evaluate(text, new Node());
    }

    public static object Evaluate(string text, Node env)
    {
        var evaluator = new Evaluator(env);

        evaluator.Evaluate(text);

        return evaluator.This!;
    }
}