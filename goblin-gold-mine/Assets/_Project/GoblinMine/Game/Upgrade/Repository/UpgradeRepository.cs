using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Upgrade.Model;

namespace _Project.GoblinMine.Game.Upgrade.Repository
{
    public class UpgradeRepository
    {
        public List<UpgradeModel> Upgrades { get; set; } = new List<UpgradeModel>();

        public UpgradeModel GetUpgradeByType(UpgradeType type)
        {
            return Upgrades.FirstOrDefault(u => u.UpgradeType == type);
        }
    }
}
