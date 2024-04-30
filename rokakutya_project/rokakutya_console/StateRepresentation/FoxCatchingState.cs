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
        public const char FOX = 'F';
        public const char DOG1 = '1';
        public const char DOG2 = '2';
        public const char DOG3 = '3';
        public const char DOG4 = '4';

        public char[,] Board { get; set; }

        public FoxCatchingState()
        {
            Board = new char[8, 8]{
                {EMPTY,EMPTY, FOX ,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,EMPTY,EMPTY,EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                {EMPTY,DOG1 ,EMPTY,DOG2 , EMPTY, DOG3 , EMPTY, DOG4 },
            };
            CurrentPlayer = FOX;
        }

        public void ChangePlayer(int index)
        {
            if (CurrentPlayer == FOX)
            {
                CurrentPlayer = index switch
                {
                    1 => DOG1,
                    2 => DOG2,
                    3 => DOG3,
                    4 => DOG4,
                    _ => throw new Exception("Got the dawg in yo?"),
                };
            }
            else
            {
                CurrentPlayer = FOX;
            }
        }






        public override object Clone()
        {
            throw new NotImplementedException();
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
