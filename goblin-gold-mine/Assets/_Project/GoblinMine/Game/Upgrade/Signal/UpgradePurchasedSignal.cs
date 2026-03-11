using ArvisGames.SignalFlow;
using _Project.GoblinMine.Game.Upgrade.Model;

namespace _Project.GoblinMine.Game.Upgrade.Signal
{
    public class UpgradePurchasedSignal : ISignal
    {
        public UpgradeType UpgradeType { get; set; }
        public int NewLevel { get; set; }
    }
}
