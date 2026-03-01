using System;
using UnityEngine;

namespace _Project.GoblinMine.Game.Resource.Model
{
    public class ResourceModel
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public string DisplayName { get; set; }
        public Material Material { get; set; }
        public int GoldValue { get; set; }
        public int CollectionAmount { get; set; }
        public float CollectionIntervalSeconds { get; set; }

        // State
        public float CollectionTimer { get; set; }
    }
}