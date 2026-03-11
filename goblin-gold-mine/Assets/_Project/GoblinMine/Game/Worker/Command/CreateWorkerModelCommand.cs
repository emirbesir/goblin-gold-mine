using System;
using _Project.GoblinMine.Game.Inventory.Model;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Model;

namespace _Project.GoblinMine.Game.Worker.Command
{
    public class CreateWorkerModelCommand
    {
        public WorkerModel Execute(ResourceType resourceType, WorkerConfiguration configuration)
        {
            var worker = new WorkerModel
            {
                Id = Guid.NewGuid(),
                ResourceType = resourceType,
                State = WorkerState.Awake,
                CollectionAmount = configuration.CollectionAmount,
                MiningIntervalSeconds = configuration.MiningIntervalSeconds,
                AwakeDurationSeconds = configuration.AwakeDurationSeconds,
                WakeUpDurationSeconds = configuration.WakeUpDurationSeconds,
                MiningRangeUnits = configuration.MiningRangeUnits,
                MiningTimer = 0f,
                AwakeTimer = 0f,
                WakeUpTimer = 0f
            };

            return worker;
        }
    }
}
