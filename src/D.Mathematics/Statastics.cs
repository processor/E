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

    public class Min : Function
    {
        public Min()
            : base("min", new Type(Kind.Any)) { }

      
    }

    public class Max : Function
    {
        public Max()
            : base("max", new Type(Kind.Any)) { }

    
    }

    public class Percentile : Function
    {
        public Percentile()
            : base("percentile", new Type(Kind.Any)) { }

       
    } // range = 0-1

    public class Quartile : Function
    {
        public Quartile()
            : base("quartile", new Type(Kind.Any)) { }

    
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

    public class Forcast : Function
    {
        public Forcast()
            : base("forcast", new Type(Kind.Any)) { }
    }
  
}
