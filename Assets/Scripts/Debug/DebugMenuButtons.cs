
using Platformer.Gameplay;
#if UNITY_EDITOR
using Platformer.Core;
using UnityEditor;
using UnityEngine;

#endif
namespace Platformer.DebugTools
{
    #if UNITY_EDITOR
    public static class DebugMenuButtons
    {
        [MenuItem("Debug/Win Level")]
        public static void DebugWinLevel()
        {
            Debug.Log("Test Win Level");
        }
        
        [MenuItem("Debug/Lose")]
        public static void DebugLoseLevel()
        {
            Debug.Log("Test Lose Level");
            Simulation.Schedule<PlayerDeath>();
        }
    }
    #endif
}