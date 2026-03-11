using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.MiningResource.View;

namespace _Project.GoblinMine.Game.MiningResource.Repository
{
    public class MiningResourceViewRepository
    {
        public List<MiningResourceView> MiningResourceViews { get; set; } = new List<MiningResourceView>();

        public MiningResourceView GetMiningResourceViewById(Guid id)
        {
            return MiningResourceViews.FirstOrDefault(v => v.Id == id);
        }
    }
}
