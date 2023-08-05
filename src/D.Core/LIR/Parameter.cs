using System.Linq.Expressions;

namespace E;

public sealed class Parameter
{
    public static readonly Parameter Object  = Get(ObjectType.Object);
    public static readonly Parameter String  = Get(ObjectType.String);
    public static readonly Parameter Byte    = Get(ObjectType.Byte);
    public static readonly Parameter Number  = Get(ObjectType.Number);
    public static readonly Parameter Decimal = Get(ObjectType.Decimal);
    public static readonly Parameter Int64   = Get(ObjectType.Int64);

    public Parameter(string name)
    {
        Name = name;
        Type = new Type(ObjectType.Object);
    }

    public Parameter(string name, ObjectType kind)
    {
        Name = name;
        Type = new Type(kind);
        Direction = ParameterDirection.In;
    }

    public Parameter(
        string name, 
        Type type, 
        bool isOptional = false,
        object? defaultValue = null,
        Annotation[]? annotations = null,
        ParameterDirection direction = ParameterDirection.In,
        Expression? condition = null)
    {
        Name         = name;
        Type         = type;
        DefaultValue = defaultValue;
        Annotations  = annotations;
        Direction    = direction;
        Condition    = condition;

        if (isOptional)
        {
            Flags |= ParameterFlags.Optional;
        }
    }

    public Parameter(Type type)
    {
        Type = type;
    }
        
    public string? Name { get; }

    public Type Type { get; }
        
    public Annotation[]? Annotations { get; }

    // Annotations

    // Unit (length)

    public ParameterFlags Flags { get; }

    public object? DefaultValue { get; }
        
    // x: Integer where value > 0 && value < 4
        
    public Expression? Condition { get; }

    public ParameterDirection Direction { get; }

    // TODO: cache on kind
    public static Parameter Get(ObjectType kind) => new(new Type(kind));

    #region Flags

    public bool IsOptional => Flags.HasFlag(ParameterFlags.Optional);

    public bool IsReadOnly => Flags.HasFlag(ParameterFlags.ReadOnly);

    #endregion
}