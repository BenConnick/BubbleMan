using System;
using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class MenuButtonEnemy : EnemyController
    {
        public enum MenuActionType
        {
            None,
            StartGame,
            Settings,
            Credits,
        }

        public MenuActionType MenuAction;
        
        public void DoMenuAction()
        {
            switch (MenuAction)
            {
                case MenuActionType.None:
                    throw new System.NotImplementedException();
                case MenuActionType.StartGame:
                    GameController.Instance.LevelLoader.LoadNextLevel();
                    break;
                case MenuActionType.Settings:
                    Debug.Log("Settings");
                    break;
                case MenuActionType.Credits:
                    Debug.Log("Credits");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                DoMenuAction();
            }
        }
    }
}