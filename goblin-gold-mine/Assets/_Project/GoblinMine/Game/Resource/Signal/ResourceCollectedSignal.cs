using ArvisGames.SignalFlow;
using GoblinMine.Game.Resource.Model;

namespace GoblinMine.Game.Resource.Signal
{
    public class ResourceCollectedSignal : ISignal
    {
        public ResourceType ResourceType { get; set; }
        public int Amount { get; set; }
    }
}