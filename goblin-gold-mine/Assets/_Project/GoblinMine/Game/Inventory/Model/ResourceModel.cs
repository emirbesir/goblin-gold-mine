using System;

namespace _Project.GoblinMine.Game.Inventory.Model
{
    public class ResourceModel
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public int Amount { get; set; }
    }
}