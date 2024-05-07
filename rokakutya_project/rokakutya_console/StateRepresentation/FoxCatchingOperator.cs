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

            if (X > 7 || X < 0 || Y > 7 || Y < 0)
            {
                return false;
            }

            if ((OldX - X != 1 || OldX - X == -1) && (OldY - Y != 1 || OldY - Y == -1))
            {
                return false;
            }

            if (foxCatchingState.CurrentPlayer == FoxCatchingState.PLAYER2)
            {
                if (OldY > Y)
                {
                    return false;
                }
            }

            return foxCatchingState.Board[X, Y] == FoxCatchingState.EMPTY &&
                foxCatchingState.CurrentPlayer == Player;
        }
    }
}
