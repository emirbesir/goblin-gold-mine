using System.Linq;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Model;
using _Project.GoblinMine.Game.Worker.Repository;
using _Project.GoblinMine.Game.Worker.View;
using UnityEngine;

namespace _Project.GoblinMine.Game.Worker.Command
{
    public class SpawnWorkerCommand
    {
        private readonly WorkerRepository _workerRepository;
        private readonly WorkerViewRepository _workerViewRepository;
        private readonly CreateWorkerModelCommand _createWorkerModelCommand;
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly WorkerConfiguration _workerConfiguration;
        private readonly WorkerVisualConfiguration _workerVisualConfiguration;
        private readonly WorkerView.Factory _workerViewFactory;

        public SpawnWorkerCommand(
            WorkerRepository workerRepository,
            WorkerViewRepository workerViewRepository,
            CreateWorkerModelCommand createWorkerModelCommand,
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            WorkerConfiguration workerConfiguration,
            WorkerVisualConfiguration workerVisualConfiguration,
            WorkerView.Factory workerViewFactory)
        {
            _workerRepository = workerRepository;
            _workerViewRepository = workerViewRepository;
            _createWorkerModelCommand = createWorkerModelCommand;
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _workerConfiguration = workerConfiguration;
            _workerVisualConfiguration = workerVisualConfiguration;
            _workerViewFactory = workerViewFactory;
        }

        public void Execute(
            System.Action<WorkerView, Collider> onTriggerStay,
            System.Action<WorkerView, Collider> onTriggerExit)
        {
            var targetResource = _miningResourceRepository.MiningResources
                .Where(r => r.RemainingDurability > 0)
                .OrderBy(_ => Random.value)
                .FirstOrDefault();

            if (targetResource == null)
                return;

            var resourceView = _miningResourceViewRepository.GetMiningResourceViewById(targetResource.Id);
            var spawnPosition = resourceView.transform.position + Vector3.right * _workerConfiguration.MiningRangeUnits * 0.8f;

            var workerView = _workerViewFactory.Create();
            workerView.SetResourceType(targetResource.ResourceType);
            workerView.transform.position = spawnPosition;

            var worker = _createWorkerModelCommand.Execute(targetResource.ResourceType, _workerConfiguration);
            worker.TargetMiningResourceId = targetResource.Id;
            worker.State = WorkerState.Sleeping;

            _workerRepository.Workers.Add(worker);

            workerView.Id = worker.Id;
            workerView.Initialize();
            workerView.OnTriggerStayAction += other => onTriggerStay(workerView, other);
            workerView.OnTriggerExitAction += other => onTriggerExit(workerView, other);
            workerView.SetSleeping(true, _workerVisualConfiguration);

            _workerViewRepository.WorkerViews.Add(workerView);
        }
    }
}
