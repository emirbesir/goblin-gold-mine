using System.Collections.Generic;
using UnityEngine;

namespace GoblinMine.Game.Resource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Resource/ResourceConfigurationCollection",
        fileName = "ResourceConfigurationCollection")]
    public class ResourceConfigurationCollection : ScriptableObject
    {
        [SerializeField] private List<ResourceConfiguration> configurations;

        public List<ResourceConfiguration> Configurations => configurations;
    }
}