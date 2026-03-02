using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Inventory.View;

namespace _Project.GoblinMine.Game.Inventory.Repository
{
    public class InventoryViewRepository
    {
        public List<ResourceView> ResourceViews { get; set; } = new List<ResourceView>();

        public ResourceView GetResourceViewById(Guid id)
        {
            return ResourceViews.FirstOrDefault(r => r.Id == id);
        }
    }
}
