using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.MiningResource.Model;

namespace _Project.GoblinMine.Game.MiningResource.Repository
{
    public class MiningResourceRepository
    {
        public List<MiningResourceModel> MiningResources { get; set; } = new List<MiningResourceModel>();
        
        public MiningResourceModel GetMiningResourceById(Guid id)
        {
            return MiningResources.FirstOrDefault(r => r.Id == id);
        }

        public MiningResourceModel GetMiningResourceByType(Inventory.Model.ResourceType resourceType)
        {
            return MiningResources.FirstOrDefault(r => r.ResourceType == resourceType);
        }
    }
}