using UnityEngine;

namespace _Project.GoblinMine.Game.Player.Command
{
    public class GetMoveDirectionCommand
    {
        private readonly DynamicJoystick _dynamicJoystick;

        public GetMoveDirectionCommand(DynamicJoystick dynamicJoystick)
        {
            _dynamicJoystick = dynamicJoystick;
        }

        public Vector3 Execute()
        {
            return new Vector3(_dynamicJoystick.Horizontal, 0f, _dynamicJoystick.Vertical);
        }
    }
}