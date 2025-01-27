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
            CurrentLevel++;
            if (CurrentLevel >= SceneManager.sceneCountInBuildSettings)
            {
                CurrentLevel = 0;
            }
            GameController.Instance.StartCoroutine(LoadLevelRoutine(CurrentLevel));
        }

        private IEnumerator LoadLevelRoutine(int sceneIndex)
        {
            int prev = SceneManager.GetActiveScene().buildIndex;
            var asyncOp = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncOp == null || asyncOp.isDone);
            DoLevelSetup();
            SceneManager.UnloadSceneAsync(prev);
        }

        public void LoadSpecificLevel(string levelName)
        {
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
            if (sceneIndex < 0)
            {
                Debug.LogError($"Cannot find scene '{levelName}'");
                return;
            }
            CurrentLevel = sceneIndex;
            GameController.Instance.StartCoroutine(LoadLevelRoutine(sceneIndex));
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