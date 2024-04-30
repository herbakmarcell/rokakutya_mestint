using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.StateRepresentation
{
    internal class FoxCatchingOperator : Operator
    {
        public FoxCatchingOperator(int x, int y, char player)
        {
            X = x;
            Y = y;
            Player = player;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Player { get; set; }

        public State Apply(State state)
        {
            if (state == null || !(state is FoxCatchingState))
            {
                throw new Exception("Fox you!");
            }

            FoxCatchingState newState = state.Clone() as FoxCatchingState;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((state as FoxCatchingState).Board[i, j]  == Player)
                    {
                        newState.Board[i, j] = FoxCatchingState.EMPTY;
                    }
                }
            }

            newState.Board[X, Y] = Player;
            newState.ChangePlayer();

            return newState;
        }

        public bool IsApplicable(State state)
        {
            if (state == null || !(state is FoxCatchingState))
            {
                return false;
            }

            FoxCatchingState foxCatchingState = state as FoxCatchingState;

            return foxCatchingState.Board[X, Y] == FoxCatchingState.EMPTY &&
                foxCatchingState.CurrentPlayer == Player;
        }
    }
}
