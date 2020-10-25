using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    struct MyExceptions
    {
        public class NonExistentCellException : Exception
        { 
            public NonExistentCellException() : base("Non-existent cell in expression") { }
        }


        public class LoopException : Exception
        {
            public LoopException() : base("Loop in expression") { }
        }


        public class DivideByZero : DivideByZeroException
        {
            public DivideByZero() : base("Division by zero") { }
        }
        

        public class UninitializedVariable : Exception
        {
            public UninitializedVariable() : base("Using an uninitialized variable") { }
        }
    }
}
