using System;
using _Project.GoblinMine.Game.Resource.Configuration;
using _Project.GoblinMine.Game.Resource.Model;

namespace _Project.GoblinMine.Game.Resource.Command
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
