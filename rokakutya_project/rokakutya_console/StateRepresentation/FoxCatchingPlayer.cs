using rokakutya_console.Interfaces;
using rokakutya_console.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.StateRepresentation
{
    public class FoxCatchingPlayer
    {
        public Solver Solver { get; set; }
        
        public FoxCatchingPlayer()
        {
            Solver = new MiniMaxWithAlphaBetaCutting(new FoxCatchingOperatorGenerator(),7);
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
                        Console.Write("New X: ");
                    } while (!int.TryParse(Console.ReadLine(), out x));

                    int y = 0;
                    do
                    {
                        Console.Write("New Y: ");
                    } while (!int.TryParse(Console.ReadLine(), out y));

                    int[] foxPosition = FoxPosition(state);

                    o = new FoxCatchingOperator(foxPosition[0], foxPosition[1], x - 1, y - 1, FoxCatchingState.PLAYER1);

                } while (!o.IsApplicable(state));

                state = o.Apply(state);

                Console.WriteLine(state);

                if (CheckStatus(state)) break;
                
                state = Solver.NextMove(state);

                Console.WriteLine(state);

                if (CheckStatus(state)) break;
            }
        }

        private int[] FoxPosition(State state)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((state as FoxCatchingState).Board[i,j] == FoxCatchingState.PLAYER1)
                    {
                        return [i, j];
                    }
                }
            }
            throw new Exception("WHO LET THE FOX OUT???");
        }

        private bool CheckStatus(State state)
        {
            if (state.GetStatus() == Status.PLAYER1WINS)
            {
                Console.WriteLine("Player 1 wins!");
                return true;
            }

            if (state.GetStatus() == Status.PLAYER2WINS)
            {
                Console.WriteLine("Player 2 wins!");
                return true;
            }

            return false;
        }

    }
}
