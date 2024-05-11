using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            return IsValidState(state) &&
                   IsNewPosition() &&
                   IsOnBoard() &&
                   IsDiagonal() &&
                   IsOneSquare() &&
                   IsEmpty(state) &&
                   IsCharacterThePlayer(state) &&
                   IsPlayerTurn(state) &&
                   (Player == 'D' ? IsDaDogMovingForward() : true);
        }
        
        private bool IsValidState(State state)
        {
            return state != null && state is FoxCatchingState;
        }
        private bool IsOnBoard()
        {
            return X >= 0 && X <= 7 && Y >= 0 && Y <= 7;
        }
        private bool IsNewPosition()
        {
            return OldX != X && OldY != Y;
        }
        private bool IsDiagonal()
        {
            return Math.Abs(OldX - X) == Math.Abs(OldY - Y);
        }
        private bool IsOneSquare()
        {
            return (Math.Abs(OldX - X) == 1) && (Math.Abs(OldY - Y) == 1);
        }
        private bool IsCharacterThePlayer(State state)
        {
            FoxCatchingState foxCatchingState = state as FoxCatchingState;

            return foxCatchingState.Board[OldX, OldY] == Player;
        }
        private bool IsEmpty(State state)
        {
            FoxCatchingState foxCatchingState = state as FoxCatchingState;

            return foxCatchingState.Board[X, Y] == FoxCatchingState.EMPTY;
        }
        private bool IsPlayerTurn(State state)
        {
            FoxCatchingState foxCatchingState = state as FoxCatchingState;
            return foxCatchingState.CurrentPlayer == Player;
        }
        private bool IsDaDogMovingForward()
        {
            return OldX > X;
        }
    }
}
