using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player picks up an enemy
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerTypeChanged : Simulation.Event<PlayerTypeChanged>
    {
        private static readonly int ActivateTrigger = Animator.StringToHash("activate");
        public PlayerController player;

        public override void Execute()
        {
            if (player.audioSource && player.powerupAudio)
                player.audioSource.PlayOneShot(player.powerupAudio);
            if (player.ColorIndicator != null)
            {
                player.ColorIndicator.color = player.GetRPS().Prev().GetHalfColor();
            }
            if (player.PowerGetEffectAnimator != null)
            {
                player.PowerGetEffectAnimator.SetTrigger(ActivateTrigger);
            }
        } 
    }
}