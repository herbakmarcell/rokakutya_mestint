using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.StateRepresentation
{
    public class FoxCatchingOperatorGenerator : OperatorGenerator
    {
        public List<Operator> Operators { get; }

        public FoxCatchingOperatorGenerator()
        {
            Operators = new List<Operator>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Operators.Add(new FoxCatchingOperator(i, j, FoxCatchingState.FOX));

                    Operators.Add(new FoxCatchingOperator(i, j, FoxCatchingState.DOG1));
                    Operators.Add(new FoxCatchingOperator(i, j, FoxCatchingState.DOG2));
                    Operators.Add(new FoxCatchingOperator(i, j, FoxCatchingState.DOG3));
                    Operators.Add(new FoxCatchingOperator(i, j, FoxCatchingState.DOG4));
                }
            }
        }
    }
}
