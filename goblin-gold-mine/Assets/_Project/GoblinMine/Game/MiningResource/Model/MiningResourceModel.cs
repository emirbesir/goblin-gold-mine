using System;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.MiningResource.Model
{
    public class MiningResourceModel
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public int EconomicValue { get; set; }
        public int CollectionAmount { get; set; }
        public float CollectionIntervalSeconds { get; set; }
        public int ChunkCount { get; set; }

        // Runtime
        public float CollectionTimer { get; set; }
    }
}