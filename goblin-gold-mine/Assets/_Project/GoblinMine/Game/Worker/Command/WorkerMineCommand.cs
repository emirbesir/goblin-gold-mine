using System.Threading;
using _Project.GoblinMine.Game.MiningResource.Command;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Model;
using _Project.GoblinMine.Game.Worker.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.GoblinMine.Game.Worker.Command
{
    public class WorkerMineCommand
    {
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly MiningResourceConfigurationCollection _miningResourceConfigurationCollection;
        private readonly CollectMiningResourceCommand _collectMiningResourceCommand;
        private readonly RespawnMiningResourceCommand _respawnMiningResourceCommand;
        private readonly WorkerVisualConfiguration _workerVisualConfiguration;

        public WorkerMineCommand(
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            MiningResourceConfigurationCollection miningResourceConfigurationCollection,
            CollectMiningResourceCommand collectMiningResourceCommand,
            RespawnMiningResourceCommand respawnMiningResourceCommand,
            WorkerVisualConfiguration workerVisualConfiguration)
        {
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _miningResourceConfigurationCollection = miningResourceConfigurationCollection;
            _collectMiningResourceCommand = collectMiningResourceCommand;
            _respawnMiningResourceCommand = respawnMiningResourceCommand;
            _workerVisualConfiguration = workerVisualConfiguration;
        }

        public void Execute(WorkerModel worker, WorkerView workerView, CancellationToken cancellationToken)
        {
            var resource = _miningResourceRepository.GetMiningResourceById(worker.TargetMiningResourceId);

            if (resource == null || resource.RemainingDurability <= 0)
                return;

            var resourceView = _miningResourceViewRepository.GetMiningResourceViewById(resource.Id);
            var distance = Vector3.Distance(workerView.transform.position, resourceView.transform.position);

            if (distance > worker.MiningRangeUnits)
                return;

            var config = _miningResourceConfigurationCollection.GetConfigurationByType(resource.ResourceType);

            workerView.PlayCollectionPulse(_workerVisualConfiguration);
            _collectMiningResourceCommand.Execute(resource, resourceView, config, cancellationToken, autoDeposit: true).Forget();

            resource.RemainingDurability--;

            if (resource.RemainingDurability <= 0)
            {
                resource.CollectionTimer = 0f;
                resourceView.SetDepleted(true);
                _respawnMiningResourceCommand.Execute(resource, resourceView, config, cancellationToken).Forget();
            }
        }
    }
}
