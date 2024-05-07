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
        public const char EMPTY = ' ';
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

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] != other.Board[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    1   2   3   4   5   6   7   8");
            for (int i = 0; i < 8; i++)
            {
                sb.AppendLine("  +---+---+---+---+---+---+---+---+");
                sb.Append(string.Format("{0} |", i + 1));
                for (int j = 0; j < 8; j++)
                {
                    sb.Append(string.Format(" {0} |", Board[i, j]));
                }
                sb.AppendLine();
            }
            sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            sb.AppendLine("Current player: " + CurrentPlayer);

            return sb.ToString();
        }

        public override Status GetStatus()
        {
            if (GetFoxPos()[0] >= GetLastDogRow())
            {
                return Status.FOXWINS;
            }

            if (FoxCantMove())
            {
                return Status.DOGSWIN;
            }

            return Status.PLAYING;
        }

        public override int GetHeuristics(char player)
        {
            throw new NotImplementedException();
        }

        private int GetLastDogRow()
        {
            int lastDogRow = -1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] == PLAYER2)
                    {
                        lastDogRow = i;
                    }
                }
            }
            return lastDogRow;
        }
        private int[] GetFoxPos()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] == PLAYER1)
                    {
                        return [i,j];
                    }
                }
            }
            throw new Exception("WHO LET THE FOX OUT???");
        }
        private bool FoxCantMove()
        {
            int[] foxPos = GetFoxPos();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int newRow = foxPos[0] + i;
                    int newColumn = foxPos[1] + j;

                    if (newRow >= 0 && newRow < 8 &&
                        newColumn >= 0 && newColumn < 8)
                    {
                        if (Board[newRow, newColumn] == EMPTY)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
