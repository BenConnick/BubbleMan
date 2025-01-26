using System;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    public class EnemyTracker
    {
        private static List<EnemyController> _QueuedEnemies = new List<EnemyController>();

        public static void TrackEnemy(EnemyController controller)
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.EnemyTracker.LivingEnemies.Add(controller);
            }
            else
            {
                _QueuedEnemies.Add(controller);
            }
        }

        public static void OnEnemyDestroyed(EnemyController controller)
        {
            if (GameController.Instance != null)
            {
                var enemyCollection = GameController.Instance.EnemyTracker.LivingEnemies;
                if (enemyCollection.Contains(controller))
                {
                    enemyCollection.Remove(controller);
                    if (enemyCollection.Count == 0)
                    {
                        GameController.Instance.EnemyTracker.EnemiesCleared?.Invoke();
                    }
                }
            }
            else
            {
                if (_QueuedEnemies.Contains(controller))
                {
                    _QueuedEnemies.Remove(controller);
                }
            }
        }
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnEnterPlayMode]
        private static void DomainReload()
        {
            _QueuedEnemies.Clear();
        }
        #endif
        
        public HashSet<EnemyController> LivingEnemies = new HashSet<EnemyController>();

        public event Action EnemiesCleared;

        public EnemyTracker()
        {
            foreach (EnemyController e in _QueuedEnemies)
            {
                LivingEnemies.Add(e);
            }
            _QueuedEnemies.Clear();
        }
    }
}