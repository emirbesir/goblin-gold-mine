using _Project.GoblinMine.Game.MiningResource.Signal;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Model;
using _Project.GoblinMine.Game.Worker.View;
using Zenject;

namespace _Project.GoblinMine.Game.Worker.Command
{
    public class WorkerMineCommand
    {
        private readonly SignalBus _signalBus;
        private readonly WorkerVisualConfiguration _workerVisualConfiguration;

        public WorkerMineCommand(
            SignalBus signalBus,
            WorkerVisualConfiguration workerVisualConfiguration)
        {
            _signalBus = signalBus;
            _workerVisualConfiguration = workerVisualConfiguration;
        }

        public void Execute(WorkerModel worker, WorkerView workerView)
        {
            workerView.PlayCollectionPulse(_workerVisualConfiguration);

            _signalBus.Fire(new ResourceCollectedSignal
            {
                ResourceType = worker.ResourceType,
                CollectionAmount = worker.CollectionAmount
            });
        }
    }
}
