using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.MiningResource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/MiningResource/MiningResourceConfigurationCollection",
        fileName = "MiningResourceConfigurationCollection")]
    public class MiningResourceConfigurationCollection : ScriptableObject
    {
        [SerializeField] private List<MiningResourceConfiguration> configurations;

        public List<MiningResourceConfiguration> Configurations => configurations;

        public MiningResourceConfiguration GetConfigurationByType(ResourceType resourceType)
        {
            return configurations.FirstOrDefault(config => config.ResourceType == resourceType);
        }
    }
}