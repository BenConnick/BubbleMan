using System;

namespace Platformer.Mechanics
{
    public static class RockPaperScissorsHelper
    {
        public static bool Beats(this TypeRPS a, TypeRPS b)
        {
            return Compare(a, b) > 0;
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
                    break;
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
                    break;
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
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(a), a, null);
            }
        }
    }
}