using Runner.Scripts.Profile;
using Runner.Scripts.Tool;

namespace Runner.Scripts.Game
{
    internal class GameController:BaseController
    {
        public GameController(ProfilePlayer profilePlayer) 
        {
            var leftMoveDif = new SubscriptionProperty<float>();
            var rightMoveDif = new SubscriptionProperty<float>();

            var tapeBackground = new TapeBackGroundgontroller(leftMoveDif, rightMoveDif);
            AddController(tapeBackground);

            var inputController = new InputController(leftMoveDif, rightMoveDif, profilePlayer.Player);
            AddController(inputController);

            var playerController = new PlayerController();
            AddController(playerController);
        }
    }
}
