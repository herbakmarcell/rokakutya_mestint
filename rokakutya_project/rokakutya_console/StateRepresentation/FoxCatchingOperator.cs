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
        public FoxCatchingOperator(int oldX, int oldY, int x, int y, char player)
        {
            OldX = oldX;
            OldY = oldY;
            X = x;
            Y = y;
            Player = player;
        }

        public int OldX { get; set; }

        public int OldY { get; set; }

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
            newState.Board[OldX, OldY] = FoxCatchingState.EMPTY;
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

            
            if ((Math.Abs(X - OldX) != 1) || (Math.Abs(Y - OldY) != 1))
            {
                return false;
            }
            if (X < 0 || X > 7 || Y < 0 || Y > 7 || OldX == X || OldY == Y)
            {
                return false;
            }

            if (foxCatchingState.CurrentPlayer == FoxCatchingState.PLAYER2)
            {
                if (foxCatchingState.Board[OldX, OldY] != FoxCatchingState.PLAYER2)
                {
                    return false;
                }
                if (OldX < X)
                {
                    return false;
                }
            }



            return foxCatchingState.Board[X, Y] == FoxCatchingState.EMPTY &&
                foxCatchingState.CurrentPlayer == Player;
        }
    }
}
