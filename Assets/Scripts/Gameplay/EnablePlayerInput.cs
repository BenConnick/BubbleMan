using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        public override void Execute()
        {
            var model = GameController.Instance.model;
            var player = model.player;
            player.controlEnabled = true;
        }
    }
}