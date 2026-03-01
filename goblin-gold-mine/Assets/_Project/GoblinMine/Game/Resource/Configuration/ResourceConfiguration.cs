using _Project.GoblinMine.Game.Resource.Model;
using UnityEngine;

namespace _Project.GoblinMine.Game.Resource.Configuration
{
    [CreateAssetMenu(
        menuName = "Configuration/Game/Resource/ResourceConfiguration",
        fileName = "ResourceConfiguration")]
    public class ResourceConfiguration : ScriptableObject
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private string displayName;
        [SerializeField] private Material material;
        [SerializeField] private int value;
        [SerializeField] private float collectionIntervalSeconds;

        public ResourceType ResourceType => resourceType;
        public string DisplayName => displayName;
        public Material Material => material;
        public int Value => value;
        public float CollectionIntervalSeconds => collectionIntervalSeconds;
    }
}