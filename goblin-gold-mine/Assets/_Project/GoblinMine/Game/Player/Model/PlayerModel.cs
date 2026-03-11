namespace _Project.GoblinMine.Game.Player.Model
{
    public class PlayerModel
    {
        public string DisplayName { get; set; }
        public float MoveSpeedUnitsPerSecond { get; set; }
        public float BaseMoveSpeedUnitsPerSecond { get; set; }
        public float MiningIntervalSeconds { get; set; }
        public float BaseMiningIntervalSeconds { get; set; }
        public int MaxCarryCapacity { get; set; }
    }
}