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

        public static Color GetHalfColor(this TypeRPS a)
        {
            return Color.Lerp(Color.white, a.GetColor(), 0.4f);
        }

        public static Color GetColor(this TypeRPS a)
        {
            Color color = Color.white;
            switch (a)
            {
                case TypeRPS.None:
                    break;
                case TypeRPS.RockRed:
                    color = Color.red;
                    break;
                case TypeRPS.PaperPurple:
                    color = new Color(.8f, 0.2f, .8f);
                    break;
                case TypeRPS.ScissorsOrange:
                    color = new Color(.8f, .6f, .2f);
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
                case TypeRPS.RockRed:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.RockRed:
                            return 0;
                        case TypeRPS.PaperPurple:
                            return -1;
                        case TypeRPS.ScissorsOrange:
                            return 1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(b), b, null);
                    }
                case TypeRPS.PaperPurple:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.RockRed:
                            return 1;
                        case TypeRPS.PaperPurple:
                            return 0;
                        case TypeRPS.ScissorsOrange:
                            return -1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(b), b, null);
                    }
                case TypeRPS.ScissorsOrange:
                    switch (b)
                    {
                        case TypeRPS.None:
                            return 0;
                        case TypeRPS.RockRed:
                            return -1;
                        case TypeRPS.PaperPurple:
                            return 1;
                        case TypeRPS.ScissorsOrange:
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