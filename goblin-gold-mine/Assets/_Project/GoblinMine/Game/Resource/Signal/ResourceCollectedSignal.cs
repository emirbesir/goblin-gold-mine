using ArvisGames.SignalFlow;
using _Project.GoblinMine.Game.Resource.Model;

namespace _Project.GoblinMine.Game.Resource.Signal
{
    public class ResourceCollectedSignal : ISignal
    {
        public ResourceType ResourceType { get; set; }
        public int Amount { get; set; }
    }
}