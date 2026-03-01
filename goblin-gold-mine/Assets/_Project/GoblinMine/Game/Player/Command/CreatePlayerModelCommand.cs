using _Project.GoblinMine.Game.Player.Configuration;
using _Project.GoblinMine.Game.Player.Model;

namespace _Project.GoblinMine.Game.Player.Command
{
    public class CreatePlayerModelCommand
    {
        private readonly PlayerConfiguration _playerConfiguration;
        
        public CreatePlayerModelCommand(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
        }

        public PlayerModel Execute()
        {
            var playerModel = new PlayerModel
            {
                DisplayName = _playerConfiguration.DisplayName,
                MoveSpeedUnitsPerSecond = _playerConfiguration.MoveSpeedUnitsPerSecond,
            };

            return playerModel;
        }
    }
}