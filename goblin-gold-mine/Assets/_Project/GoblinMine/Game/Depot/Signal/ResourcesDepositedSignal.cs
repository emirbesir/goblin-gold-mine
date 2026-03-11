using ArvisGames.SignalFlow;

namespace _Project.GoblinMine.Game.Depot.Signal
{
    public class ResourcesDepositedSignal : ISignal
    {
        public int GoldEarned { get; set; }
    }
}
