using System;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {

        public override void Execute()
        {
            var model = GameController.Instance.model;
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = false;
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            player.health.Increment();
            Vector3 spawnPoint = Vector3.zero;
            if (model.spawnPoint == null)
            {
                var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (GameObject rootGameObject in roots)
                {
                    Transform root = rootGameObject.transform;
                    if (string.Equals(root.name, "SpawnPoint", StringComparison.OrdinalIgnoreCase))
                    {
                        model.spawnPoint = root;
                        break;
                    }
                }
            }
            if (model.spawnPoint != null) spawnPoint = model.spawnPoint.position;
            player.Teleport(spawnPoint);
            player.jumpState = PlayerController.JumpState.Grounded;
            player.animator.SetBool("dead", false);
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}