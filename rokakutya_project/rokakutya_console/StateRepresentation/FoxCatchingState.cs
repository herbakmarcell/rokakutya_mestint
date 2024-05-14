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
                {EMPTY,    EMPTY, PLAYER1 ,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY,    EMPTY,    EMPTY,    EMPTY, EMPTY,     EMPTY, EMPTY,     EMPTY},
                {EMPTY, PLAYER2 ,    EMPTY, PLAYER2 , EMPTY,  PLAYER2 , EMPTY,  PLAYER2 },
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
                return Status.PLAYER1WINS;
            }

            if (FoxCantMove())
            {
                return Status.PLAYER2WINS;
            }

            return Status.PLAYING;
        }

        private static int WIN = 100;
        private static int LOSE = -100;

        private static int POSITIONAL_ADVANCE = 2;
        private static int POSITIONAL_ADVANCE_DOG = 1;

        private static int DOG_BLOCKING_FOX = 9;

        private static int DOG_BLOCKING_DOG = 3;

        private static int FOX_PATH_BLOCKED = 4;

        private static int PASSED_DOG = 10;



        public override int GetHeuristics(char player)
        {
            if (GetStatus() == Status.PLAYER1WINS && player == PLAYER1) return WIN;
            if (GetStatus() == Status.PLAYER2WINS && player == PLAYER2) return WIN;

            if (GetStatus() == Status.PLAYER1WINS && player != PLAYER1) return LOSE;
            if (GetStatus() == Status.PLAYER2WINS && player != PLAYER2) return LOSE;

            int result = 0;

            if (player == PLAYER1)
            {
                result += FoxPostitionalAdvance();
                result -= DogPostitionalAdvance();
                result -= CheckDogsOnFox();
                result += DogBlockedByOtherDog();
                result -= DogBlockingFoxPath();
                result += PassedDogs();
            }
            else
            {
                result -= FoxPostitionalAdvance();
                result += DogPostitionalAdvance();
                result += CheckDogsOnFox();
                result -= DogBlockedByOtherDog();
                result += DogBlockingFoxPath();
                result -= PassedDogs();
            }

            return result;
        }

        private int PassedDogs()
        {
            int subResult = 0;
            int[] foxPos = GetFoxPos();
            List<int[]> dogPositions = GetDogPositions();
            foreach (int[] dogPos in dogPositions)
            {
                if (dogPos[0] <= foxPos[0])
                {
                    subResult += PASSED_DOG;
                }
            }

            return subResult;
        }
        private int DogBlockingFoxPath()
        {
            int subResult = 0;
            int[] foxPos = GetFoxPos();
            List<int[]> dogPositions = GetDogPositions();
            foreach (int[] dogPos in dogPositions)
            {
                if (Math.Abs(foxPos[0] - dogPos[0]) == Math.Abs(foxPos[1] - dogPos[1]))
                {
                    subResult += FOX_PATH_BLOCKED;
                }
            }

            return subResult;
        }
        private int DogBlockedByOtherDog()
        {
            List<int[]> dogPositions = GetDogPositions();
            int subResult = 0;
            foreach (int[] dogPos in dogPositions)
            {
                if ((dogPos[0] + 1 < 8 && dogPos[1] + 1 < 8) && (dogPos[0] - 1 > 0 && dogPos[1] - 1 > 0))
                {
                    if (Board[dogPos[0] - 1, dogPos[1] + 1] == PLAYER2)
                    {
                        subResult += DOG_BLOCKING_DOG;
                    }
                    if (Board[dogPos[0] - 1, dogPos[1] - 1] == PLAYER2)
                    {
                        subResult += DOG_BLOCKING_DOG;
                    }
                }
            }
            return subResult;
        }
        private int CheckDogsOnFox()
        {
            int subResult = 0;
            int[] foxPos = GetFoxPos();
            if ((foxPos[0] + 1 < 8 && foxPos[1] + 1 < 8) && (foxPos[0] - 1 > 0 && foxPos[1] - 1 > 0))
            {
                if (Board[foxPos[0] + 1, foxPos[1] + 1] == PLAYER2)
                {
                    subResult += DOG_BLOCKING_FOX;
                }
                if (Board[foxPos[0] + 1, foxPos[1] - 1] == PLAYER2)
                {
                    subResult += DOG_BLOCKING_FOX;
                }
                if (Board[foxPos[0] - 1, foxPos[1] + 1] == PLAYER2)
                {
                    subResult += DOG_BLOCKING_FOX;
                }
                if (Board[foxPos[0] - 1, foxPos[1] - 1] == PLAYER2)
                {
                    subResult += DOG_BLOCKING_FOX;
                }
            }

            return subResult;
        }
        private int FoxPostitionalAdvance()
        {
            int subResult = 0;
            int[] foxPos = GetFoxPos();
            
            subResult += foxPos[1] * POSITIONAL_ADVANCE;

            return subResult;
        }   
        private int DogPostitionalAdvance() 
        {
            int subResult = 0;
            List<int[]> dogPositions = GetDogPositions();
            foreach (int[] dogPos in dogPositions)
            {
                subResult += (7 - dogPos[0]) * POSITIONAL_ADVANCE_DOG;
            }

            return subResult;
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
        private List<int[]> GetDogPositions() 
        {
            List<int[]> positions = new List<int[]>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i,j] == PLAYER2)
                    {
                        positions.Add(new int[] { i, j });
                    }
                }
            }

            return positions;
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
