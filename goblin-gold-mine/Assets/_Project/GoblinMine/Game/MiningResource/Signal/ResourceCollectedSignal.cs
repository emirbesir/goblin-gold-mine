using ArvisGames.SignalFlow;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.MiningResource.Signal
{
    public class ResourceCollectedSignal : ISignal
    {
        public ResourceType ResourceType { get; set; }
        public int CollectionAmount { get; set; }
    }
}