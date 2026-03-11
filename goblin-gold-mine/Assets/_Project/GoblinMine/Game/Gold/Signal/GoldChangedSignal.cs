using ArvisGames.SignalFlow;

namespace _Project.GoblinMine.Game.Gold.Signal
{
    public class GoldChangedSignal : ISignal
    {
        public int NewAmount { get; set; }
        public int Delta { get; set; }
    }
}
