using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.Inventory.Repository
{
    public class InventoryRepository
    {
        public List<ResourceModel> Resources { get; set; } = new List<ResourceModel>();

        public ResourceModel GetResourceById(Guid id)
        {
            return Resources.FirstOrDefault(r => r.Id == id);
        }

        public ResourceModel GetResourceByType(ResourceType type)
        {
            return Resources.FirstOrDefault(r => r.ResourceType == type);
        }
    }
}
