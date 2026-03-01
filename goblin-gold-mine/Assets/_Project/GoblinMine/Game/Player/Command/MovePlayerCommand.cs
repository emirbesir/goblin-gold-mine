using GoblinMine.Game.Player.Repository;
using GoblinMine.Game.Player.View;
using UnityEngine;

namespace GoblinMine.Game.Player.Command
{
    public class MovePlayerCommand
    {
        private readonly PlayerRepository _playerRepository;
        private readonly PlayerView _playerView;

        public MovePlayerCommand(
            PlayerRepository playerRepository,
            PlayerView playerView)
        {
            _playerRepository = playerRepository;
            _playerView = playerView;
        }

        public void Execute(Vector3 moveDirection)
        {
            var moveAmount = moveDirection * _playerRepository.Player.MoveSpeedUnitsPerSecond * Time.deltaTime;
            var newPosition = _playerView.transform.position + moveAmount;
            _playerView.SetPosition(newPosition);
        }
    }
}
