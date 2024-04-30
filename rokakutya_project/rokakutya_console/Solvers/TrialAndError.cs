using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.Solvers
{
    public class TrialAndError : Solver
    {
        public TrialAndError(OperatorGenerator operatorGenerator) : base(operatorGenerator)
        {
        }
        private Random rnd = new Random();

        private Operator SelectOperator(State currentState)
        {
            List<int> indexes = new List<int>();
            while (indexes.Count < Operators.Count)
            {
                int index = rnd.Next(0, Operators.Count);
                while (indexes.Contains(index))
                {
                    index = rnd.Next(0, Operators.Count);
                }
                indexes.Add(index);
            }

            foreach (int index in indexes)
            {
                if (Operators[index].IsApplicable(currentState))
                {
                    return Operators[index];
                }
            }

            return null;
        }
        public override State NextMove(State state)
        {
            Operator op = SelectOperator(state);
            if (op != null)
            {
                return op.Apply(state);
            }

            return null;
        }
    }
}
