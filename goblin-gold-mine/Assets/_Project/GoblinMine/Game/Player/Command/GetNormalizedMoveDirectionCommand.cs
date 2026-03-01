using UnityEngine;

namespace GoblinMine.Game.Player.Command
{
    public class GetNormalizedMoveDirectionCommand
    {
        private readonly DynamicJoystick _dynamicJoystick;

        public GetNormalizedMoveDirectionCommand(DynamicJoystick dynamicJoystick)
        {
            _dynamicJoystick = dynamicJoystick;
        }

        public Vector3 Execute()
        {
            var moveDirection = new Vector3(_dynamicJoystick.Horizontal, 0f, _dynamicJoystick.Vertical);
            return moveDirection.normalized;
        }
    }
}