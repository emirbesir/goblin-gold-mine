using System;
using _Project.GoblinMine.Game.Inventory.Model;

namespace _Project.GoblinMine.Game.Worker.Model
{
    public class WorkerModel
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public WorkerState State { get; set; }
        public int CollectionAmount { get; set; }
        public float MiningIntervalSeconds { get; set; }
        public float AwakeDurationSeconds { get; set; }
        public float WakeUpDurationSeconds { get; set; }
        public float MiningTimer { get; set; }
        public float AwakeTimer { get; set; }
        public float WakeUpTimer { get; set; }
    }
}
