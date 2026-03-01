using System;
using UnityEngine;

namespace GoblinMine.Game.Resource.Model
{
    public class ResourceModel
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public Material Material { get; set; }
        public string DisplayName { get; set; }
        public int Value { get; set; }
        public float CollectionIntervalSeconds { get; set; }

        // State
        public float CollectionTimer { get; set; }
    }
}