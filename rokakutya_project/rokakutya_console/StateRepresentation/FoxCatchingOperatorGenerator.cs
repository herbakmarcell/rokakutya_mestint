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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            
                            Operators.Add(new FoxCatchingOperator(i, j, k, l, FoxCatchingState.PLAYER1));
                            Operators.Add(new FoxCatchingOperator(i, j, k, l, FoxCatchingState.PLAYER2));
                        }
                    }
                    
                }
            }
        }
    }
}
