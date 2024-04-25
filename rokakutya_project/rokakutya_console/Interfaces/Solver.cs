using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.Interfaces
{
    public abstract class Solver
    {
        public List<Operator> Operators { get; set; }
        protected Solver(OperatorGenerator operatorGenerator)
        {
            Operators = operatorGenerator.Operators;
        }
        public abstract State NextMove(State state);
    }
}
