using System;
using System.Collections;
using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    public class LevelSystem
    {
        public int CurrentLevel { get; private set; }
        
        public void LoadNextLevel()
        {
            GameController.Instance.StartCoroutine(LoadNextLevelRoutine());
        }

        private IEnumerator LoadNextLevelRoutine()
        {
            CurrentLevel++;
            int nextLevelSceneIndex = CurrentLevel;
            var asyncOp = SceneManager.LoadSceneAsync(nextLevelSceneIndex, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncOp == null || asyncOp.isDone);
            Simulation.Schedule<PlayerSpawn>();
            int prevLevelSceneIndex = CurrentLevel-1; // zero indexed
            SceneManager.UnloadSceneAsync(prevLevelSceneIndex);
        }
    }
}