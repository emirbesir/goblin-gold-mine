using UnityEngine;

namespace GoblinMine.Game.Player.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Player/PlayerConfiguration",
        fileName = "PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private float moveSpeedUnitsPerSecond;

        public string DisplayName => displayName;
        public float MoveSpeedUnitsPerSecond => moveSpeedUnitsPerSecond;
    }
}