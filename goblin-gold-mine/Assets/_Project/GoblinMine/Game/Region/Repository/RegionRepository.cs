using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Region.Model;

namespace _Project.GoblinMine.Game.Region.Repository
{
    public class RegionRepository
    {
        public List<RegionModel> Regions { get; set; } = new List<RegionModel>();

        public RegionModel GetRegionByIndex(int index)
        {
            return Regions.FirstOrDefault(r => r.RegionIndex == index);
        }
    }
}
