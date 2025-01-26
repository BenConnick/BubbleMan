using System;
using UnityEngine;

namespace Platformer.Mechanics
{
    public static class RockPaperScissorsHelper
    {
        public static bool Beats(this TypeRPS a, TypeRPS b)
        {
            return Compare(a, b) > 0;
        }

        public static Color GetColor(TypeRPS a)
        {
            Color color = Color.white;
            switch (a)
            {
                case TypeRPS.None:
                    break;
                case TypeRPS.Rock:
                    color = Color.red;
                    break;
                case TypeRPS.Paper:
                    color = Color.yellow;
                    break;
                case TypeRPS.Scissors:
                    color = Color.blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return color;
        }
        
        public static int Compare(TypeRPS a, TypeRPS b)
        {
            switch (a)
            {
                case TypeRPS.None:
                    return 0;
                case TypeRPS.Rock:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.Rock:
                            return 0;
                        case TypeRPS.Paper:
                            return -1;
                        case TypeRPS.Scissors:
                            return 1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(b), b, null);
                    }
                case TypeRPS.Paper:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.Rock:
                            return 1;
                        case TypeRPS.Paper:
                            return 0;
                        case TypeRPS.Scissors:
                            return -1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(b), b, null);
                    }
                case TypeRPS.Scissors:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.Rock:
                            return -1;
                        case TypeRPS.Paper:
                            return 1;
                        case TypeRPS.Scissors:
                            return 0;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(b), b, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(a), a, null);
            }
        }
    }
}