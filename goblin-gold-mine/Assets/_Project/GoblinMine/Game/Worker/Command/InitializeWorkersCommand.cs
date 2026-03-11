using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Repository;
using _Project.GoblinMine.Game.Worker.View;
using UnityEngine;

namespace _Project.GoblinMine.Game.Worker.Command
{
    public class InitializeWorkersCommand
    {
        private readonly WorkerRepository _workerRepository;
        private readonly WorkerViewRepository _workerViewRepository;
        private readonly CreateWorkerModelCommand _createWorkerModelCommand;
        private readonly MiningResourceRepository _miningResourceRepository;
        private readonly MiningResourceViewRepository _miningResourceViewRepository;
        private readonly List<WorkerView> _sceneViews;

        public InitializeWorkersCommand(
            WorkerRepository workerRepository,
            WorkerViewRepository workerViewRepository,
            CreateWorkerModelCommand createWorkerModelCommand,
            MiningResourceRepository miningResourceRepository,
            MiningResourceViewRepository miningResourceViewRepository,
            List<WorkerView> sceneViews)
        {
            _workerRepository = workerRepository;
            _workerViewRepository = workerViewRepository;
            _createWorkerModelCommand = createWorkerModelCommand;
            _miningResourceRepository = miningResourceRepository;
            _miningResourceViewRepository = miningResourceViewRepository;
            _sceneViews = sceneViews;
        }

        public void Execute(
            WorkerConfiguration configuration,
            WorkerVisualConfiguration visualConfiguration,
            Action<WorkerView, Collider> onTriggerStay,
            Action<WorkerView, Collider> onTriggerExit)
        {
            foreach (var view in _sceneViews)
            {
                var worker = _createWorkerModelCommand.Execute(view.ResourceType, configuration);

                var targetResource = _miningResourceRepository.MiningResources
                    .Where(r => r.ResourceType == view.ResourceType)
                    .OrderBy(r =>
                    {
                        var resourceView = _miningResourceViewRepository.GetMiningResourceViewById(r.Id);
                        return Vector3.Distance(view.transform.position, resourceView.transform.position);
                    })
                    .FirstOrDefault();

                if (targetResource != null)
                {
                    worker.TargetMiningResourceId = targetResource.Id;
                }

                _workerRepository.Workers.Add(worker);

                view.Id = worker.Id;
                view.Initialize();
                view.OnTriggerStayAction += other => onTriggerStay(view, other);
                view.OnTriggerExitAction += other => onTriggerExit(view, other);

                _workerViewRepository.WorkerViews.Add(view);
            }
        }
    }
}
