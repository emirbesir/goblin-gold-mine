using ArvisGames.SignalFlow;

namespace _Project.GoblinMine.Game.Region.Signal
{
    public class RegionUnlockedSignal : ISignal
    {
        public int RegionIndex { get; set; }
    }
}
