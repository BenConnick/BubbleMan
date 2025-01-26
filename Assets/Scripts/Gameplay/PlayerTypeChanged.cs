using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player picks up an enemy
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerTypeChanged : Simulation.Event<PlayerTypeChanged>
    {
        public PlayerController player;

        public override void Execute()
        {
            if (player.audioSource && player.pickupAudio)
                player.audioSource.PlayOneShot(player.pickupAudio);
        } 
    }
}