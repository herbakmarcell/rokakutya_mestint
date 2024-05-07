using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.StateRepresentation
{
    internal class FoxCatchingState : State
    {
        public const char EMPTY = 'X';
        public const char PLAYER1 = 'F';
        public const char PLAYER2 = 'D';

        public char[,] Board { get; set; }

        public FoxCatchingState()
        {
            Board = new char[8, 8]{
                {EMPTY,EMPTY, PLAYER1 ,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY, PLAYER2 ,EMPTY, PLAYER2 , EMPTY,  PLAYER2 , EMPTY,  PLAYER2 },
            };
            CurrentPlayer = PLAYER1;
        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == PLAYER1)
            {
                CurrentPlayer = PLAYER2;
            }
            else
            {
                CurrentPlayer = PLAYER1;
            }
        }

        public override object Clone()
        {
            FoxCatchingState newState = new FoxCatchingState();

            newState.Board = Board.Clone() as char[,];

            newState.CurrentPlayer = CurrentPlayer;

            return newState;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is FoxCatchingState))
            {
                return false;
            }

            FoxCatchingState other = obj as FoxCatchingState;

            if (CurrentPlayer != other.CurrentPlayer)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != other.Board[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHeuristics(char player)
        {
            throw new NotImplementedException();
        }

        public override Status GetStatus()
        {
            throw new NotImplementedException();
        }
    }
}
