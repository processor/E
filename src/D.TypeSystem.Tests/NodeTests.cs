﻿namespace E.Inference.Tests;

public class NodeTests
{
    [Fact]
    public void LetTest()
    {
        var flow = new Flow();

        var boolean = flow.GetType(ObjectType.Boolean);
        var i32     = flow.GetType(ObjectType.Int32);
        var i64     = flow.GetType(ObjectType.Int64);

        // var b = Node.Var("b");

        // var b = flow.AddVariable("b", Kind.Int32);

        var b = flow.Define("b", Type.Get(ObjectType.Int64));

        var letNode = Node.Let([
            Define(Variable("a", boolean), new ConstantNode(boolean)),
            Define(b, new ConstantNode(i64))
        ], b);

        Assert.Equal(KnownTypeNames.Int64, flow.Infer(letNode).ToString());
        
        Assert.Equal("(a = { Boolean }; b = { Int64 }) b", letNode.ToString());
    }

    [Fact]
    public void AbstractTest()
    {
        var flow = new Flow();

        var binary = flow.NewGeneric();
        var boolean = flow.GetType(ObjectType.Boolean);

        var a = Define(Variable("gt"), Node.Abstract([
            Variable("lhs", binary),
            Variable("rhs", binary)
        ], new ConstantNode(boolean), boolean));

        Assert.Equal("gt = (lhs, rhs) { Boolean } -> Boolean", a.ToString());
    }

    [Fact]
    public void AddTest()
    {
        var flow = new Flow();

        var i32 = flow.GetType(ObjectType.Int32);
        var i64 = flow.GetType(ObjectType.Int64);

        var apply = Node.Apply(Variable("+"), [
            new ConstantNode(i32), 
            new ConstantNode(i64) 
        ]);
   
        Assert.Equal("+ ({ Int32 }, { Int64 })", apply.ToString());

        Assert.Equal(KnownTypeNames.Int32, flow.Infer(apply).ToString());
    }

    private static VariableNode Variable(string name, IType type = null)
    {
        return new VariableNode(name, type);
    }

    private static DefineNode Define(VariableNode variable, INode body)
    {
        return new DefineNode(variable, body);
    }
}