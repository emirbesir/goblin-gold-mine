using _Project.Shared.Initializable;
using _Project.GoblinMine.Game.Player.Command;
using _Project.GoblinMine.Game.Player.Repository;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Player.Controller
{
    public class PlayerController : IPreInitializable, ITickable
    {
        private readonly PlayerRepository _playerRepository;
        private readonly CreatePlayerModelCommand _createPlayerModelCommand;
        private readonly MovePlayerCommand _movePlayerCommand;
        private readonly GetNormalizedMoveDirectionCommand _getNormalizedMoveDirectionCommand;

        public PlayerController(
            PlayerRepository playerRepository,
            CreatePlayerModelCommand createPlayerModelCommand,
            MovePlayerCommand movePlayerCommand,
            GetNormalizedMoveDirectionCommand getNormalizedMoveDirectionCommand)
        {
            _playerRepository = playerRepository;
            _createPlayerModelCommand = createPlayerModelCommand;
            _movePlayerCommand = movePlayerCommand;
            _getNormalizedMoveDirectionCommand = getNormalizedMoveDirectionCommand;
        }

        public void PreInitialize()
        {
            var player = _createPlayerModelCommand.Execute();
            _playerRepository.Player = player;
        }

        public void Tick()
        {
            var moveDirection = _getNormalizedMoveDirectionCommand.Execute();

            if (Mathf.Abs(moveDirection.magnitude) > 0.01f)
            {
                _movePlayerCommand.Execute(moveDirection);
            }
        }
    }
}