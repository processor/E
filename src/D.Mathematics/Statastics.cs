using System;

namespace D.Functions
{
    // Functions

    #region Count & Frequency

        /*
    public class Mean : FunctionBase
    {
        public IObject Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }
    }     // regular, geometric, harmean, trimean

    public class Median : FunctionBase
    {
        public IObject Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }
    }

    public class Mode : FunctionBase
    {
        public IObject Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }
    }

    public class Count : FunctionBase
    {
        public IObject Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }
    }
    */

    #endregion

    public class Min : FunctionExpression
    {
        public Min()
            : base("min", new Type(ObjectType.Object)) { }

      
    }

    public class Max : FunctionExpression
    {
        public Max()
            : base("max", new Type(ObjectType.Object)) { }

    
    }

    public class Percentile : FunctionExpression
    {
        public Percentile()
            : base("percentile", new Type(ObjectType.Object)) { }

       
    } // range = 0-1

    public class Quartile : FunctionExpression
    {
        public Quartile()
            : base("quartile", new Type(ObjectType.Object)) { }

    
    } // range = 0-1

    /*
    public class Rank : IFunction
    {
        public TyoBefinition Definition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }

    public class Average : IFunction
    {
        public FunctionDefinition Definition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }    // regular, deviation
    */

    public class Forcast : FunctionExpression
    {
        public Forcast()
            : base("forcast", new Type(ObjectType.Object)) { }
    }
  
}
