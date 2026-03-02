using System;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.Inventory.Command
{
    public class CreateResourceModelCommand
    {
        public ResourceModel Execute(ResourceType resourceType)
        {       
            var resource = new ResourceModel
            {
                Id = Guid.NewGuid(),
                ResourceType = resourceType,
                Amount = 0
            };
            
            return resource;
        }
    }
}
