using System;
using System.Collections;
using Cinemachine;
using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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
            DoLevelSetup();
            int prevLevelSceneIndex = CurrentLevel-1; // zero indexed
            SceneManager.UnloadSceneAsync(prevLevelSceneIndex);
        }

        private void DoLevelSetup()
        {
            Simulation.Schedule<PlayerSpawn>();

            var vCam = Object.FindObjectOfType<CinemachineVirtualCamera>();
            if (vCam != null)
            {
                vCam.m_Follow = GameController.Instance.model.player.transform;
                vCam.m_LookAt = GameController.Instance.model.player.transform;
            }
            
            var rps = GameController.Instance.model.player.GetComponent<RockPaperScissorsComponent>();
            if (rps != null)
            {
                rps.Value = TypeRPS.RockRed;
            }
        }
    }
}