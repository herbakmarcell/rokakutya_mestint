using rokakutya_console.Interfaces;
using rokakutya_console.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.StateRepresentation
{
    internal class FoxCatchingPlayer
    {
        public Solver Solver { get; set; }

        public FoxCatchingPlayer()
        {
            Solver = new MiniMax(new FoxCatchingOperatorGenerator(), 3);
        }

        public void Play()
        {
            State state = new FoxCatchingState();

            Console.WriteLine(state);

            while (state.GetStatus() == Status.PLAYING)
            {
                Operator o;
                do
                {
                    int x = 0;
                    do
                    {
                        Console.Write("X: ");
                    } while (!int.TryParse(Console.ReadLine(), out x));

                    int y = 0;
                    do
                    {
                        Console.Write("Y: ");
                    } while (!int.TryParse(Console.ReadLine(), out y));

                    o = new FoxCatchingOperator(x - 1, y - 1, FoxCatchingState.PLAYER1);

                } while (!o.IsApplicable(state));

                state = o.Apply(state);

                Console.WriteLine(state);

                if (CheckStatus(state)) break;

                state = Solver.NextMove(state);

                Console.WriteLine(state);

                if (CheckStatus(state)) break;
            }
        }

        private bool CheckStatus(State state)
        {
            if (state.GetStatus() == Status.FOXWINS)
            {
                Console.WriteLine("Player 1 wins!");
                return true;
            }

            if (state.GetStatus() == Status.DOGSWIN)
            {
                Console.WriteLine("Player 2 wins!");
                return true;
            }

            return false;
        }

    }
}
