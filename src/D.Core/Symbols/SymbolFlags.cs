namespace D
{
    public enum SymbolFlags
    {
        None        = 0,
      
        Property    = 1 << 1, // type member

        Infix       = 1 << 2,
        Postfix     = 1 << 3,

        // Scope
        BlockScoped = 1 << 4, // { } 
        Local       = 1 << 5, 
        // for / while / ...


        /*
        Variable = 1 << 20,
        Argument = 1 << 21,
        Type     = 1 << 22, // class | struct
        Function = 1 << 23,
        Module   = 1 << 24,
        Label    = 1 << 25, // State?
        Operator = 1 << 25,
        */
    }
}