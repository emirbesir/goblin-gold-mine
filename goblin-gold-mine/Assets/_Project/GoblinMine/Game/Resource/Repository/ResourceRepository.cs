using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Resource.Model;

namespace _Project.GoblinMine.Game.Resource.Repository
{
    public class ResourceRepository
    {
        public List<ResourceModel> Resources { get; set; } = new List<ResourceModel>();
        public Dictionary<ResourceType, int> TotalCollected { get; set; } = new Dictionary<ResourceType, int>();

        public ResourceModel GetResourceById(Guid id)
        {
            return Resources.FirstOrDefault(r => r.Id == id);
        }
    }
}