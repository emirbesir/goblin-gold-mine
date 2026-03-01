using System;
using GoblinMine.Game.Resource.Configuration;
using GoblinMine.Game.Resource.Model;

namespace GoblinMine.Game.Resource.Command
{
    public class CreateResourceModelCommand
    {
        public ResourceModel Execute(ResourceConfiguration resourceConfiguration)
        {
            var resource = new ResourceModel
            {
                Id = Guid.NewGuid(),
                ResourceType = resourceConfiguration.ResourceType,
                DisplayName = resourceConfiguration.DisplayName,
                Value = resourceConfiguration.Value,
                CollectionIntervalSeconds = resourceConfiguration.CollectionIntervalSeconds,
                CollectionTimer = 0f
            };
            
            return resource;
        }
    }
}
