using UnityEngine;

namespace _Project.GoblinMine.Game.Player.Command
{
    public class GetMoveDirectionCommand
    {
        private readonly Joystick _joystick;

        public GetMoveDirectionCommand(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector3 Execute()
        {
            return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
        }
    }
}