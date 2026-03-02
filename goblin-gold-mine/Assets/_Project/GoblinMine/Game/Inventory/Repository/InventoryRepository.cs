using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.Inventory.Repository
{
    public class InventoryRepository
    {
        public List<ResourceModel> Resources { get; set; } = new List<ResourceModel>();

        public ResourceModel GetResourceByType(ResourceType type)
        {
            return Resources.FirstOrDefault(r => r.ResourceType == type);
        }
    }
}
