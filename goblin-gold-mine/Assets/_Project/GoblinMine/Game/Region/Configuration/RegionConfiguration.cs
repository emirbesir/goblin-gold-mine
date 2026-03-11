using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.GoblinMine.Game.Region.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Region/RegionConfiguration",
        fileName = "RegionConfiguration")]
    public class RegionConfiguration : ScriptableObject
    {
        [SerializeField] private List<RegionEntry> regions = new List<RegionEntry>();

        public List<RegionEntry> Regions => regions;

        [Serializable]
        public class RegionEntry
        {
            public string displayName;
            public int unlockCost;
        }
    }
}
