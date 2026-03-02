using System;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Model;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class CreateMiningResourceModelCommand
    {
        public MiningResourceModel Execute(MiningResourceConfiguration resourceConfiguration)
        {
            var miningResource = new MiningResourceModel
            {
                Id = Guid.NewGuid(),
                ResourceType = resourceConfiguration.ResourceType,
                EconomicValue = resourceConfiguration.EconomicValue,
                CollectionAmount = resourceConfiguration.CollectionAmount,
                CollectionIntervalSeconds = resourceConfiguration.CollectionIntervalSeconds,
                ChunkCount = resourceConfiguration.ChunkCount,
                CollectionTimer = 0f
            };
            
            return miningResource;
        }
    }
}
