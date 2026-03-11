using System;
using System.Collections.Generic;
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
        private readonly List<WorkerView> _sceneViews;

        public InitializeWorkersCommand(
            WorkerRepository workerRepository,
            WorkerViewRepository workerViewRepository,
            CreateWorkerModelCommand createWorkerModelCommand,
            List<WorkerView> sceneViews)
        {
            _workerRepository = workerRepository;
            _workerViewRepository = workerViewRepository;
            _createWorkerModelCommand = createWorkerModelCommand;
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
