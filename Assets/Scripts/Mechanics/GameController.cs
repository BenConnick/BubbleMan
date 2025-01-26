using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        [Header("Child References")]
        public PlatformerModel model;
        public EnemyTracker EnemyTracker;
        public LevelSystem LevelLoader;

        [Header("Asset References")]
        public GameObject EventSystemPrefab;

        void OnEnable()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instantiate(EventSystemPrefab, transform);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LevelLoader ??= new LevelSystem();
            EnemyTracker ??= new EnemyTracker();
            EnemyTracker.EnemiesCleared += LevelLoader.LoadNextLevel;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }
    }
}