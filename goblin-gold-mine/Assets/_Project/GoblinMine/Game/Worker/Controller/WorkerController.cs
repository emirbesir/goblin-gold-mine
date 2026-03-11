using System;
using System.Threading;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.Worker.Command;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Model;
using _Project.GoblinMine.Game.Worker.Repository;
using _Project.GoblinMine.Game.Worker.Signal;
using _Project.GoblinMine.Game.Worker.View;
using _Project.Shared.Initializable;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Worker.Controller
{
    public class WorkerController : IPreInitializable, ITickable, IDisposable
    {
        private readonly WorkerConfiguration _workerConfiguration;
        private readonly WorkerVisualConfiguration _workerVisualConfiguration;
        private readonly WorkerRepository _workerRepository;
        private readonly WorkerViewRepository _workerViewRepository;
        private readonly InitializeWorkersCommand _initializeWorkersCommand;
        private readonly WorkerMineCommand _workerMineCommand;
        private readonly SignalBus _signalBus;

        private CancellationTokenSource _cancellationTokenSource;

        public WorkerController(
            WorkerConfiguration workerConfiguration,
            WorkerVisualConfiguration workerVisualConfiguration,
            WorkerRepository workerRepository,
            WorkerViewRepository workerViewRepository,
            InitializeWorkersCommand initializeWorkersCommand,
            WorkerMineCommand workerMineCommand,
            SignalBus signalBus)
        {
            _workerConfiguration = workerConfiguration;
            _workerVisualConfiguration = workerVisualConfiguration;
            _workerRepository = workerRepository;
            _workerViewRepository = workerViewRepository;
            _initializeWorkersCommand = initializeWorkersCommand;
            _workerMineCommand = workerMineCommand;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _initializeWorkersCommand.Execute(
                _workerConfiguration,
                _workerVisualConfiguration,
                HandleTriggerStay,
                HandleTriggerExit);
        }

        public void Tick()
        {
            foreach (var worker in _workerRepository.Workers)
            {
                if (worker.State != WorkerState.Awake)
                    continue;

                worker.MiningTimer += Time.deltaTime;

                if (worker.MiningTimer >= worker.MiningIntervalSeconds)
                {
                    worker.MiningTimer -= worker.MiningIntervalSeconds;

                    var workerView = _workerViewRepository.GetWorkerViewById(worker.Id);
                    _workerMineCommand.Execute(worker, workerView, _cancellationTokenSource.Token);
                }

                worker.AwakeTimer += Time.deltaTime;

                if (worker.AwakeTimer >= worker.AwakeDurationSeconds)
                {
                    PutWorkerToSleep(worker);
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void PutWorkerToSleep(WorkerModel worker)
        {
            worker.State = WorkerState.Sleeping;
            worker.MiningTimer = 0f;
            worker.AwakeTimer = 0f;
            worker.WakeUpTimer = 0f;

            var workerView = _workerViewRepository.GetWorkerViewById(worker.Id);
            workerView.SetSleeping(true, _workerVisualConfiguration);

            _signalBus.Fire(new WorkerStateChangedSignal
            {
                WorkerId = worker.Id,
                State = WorkerState.Sleeping
            });
        }

        private void WakeUpWorker(WorkerModel worker)
        {
            worker.State = WorkerState.Awake;
            worker.MiningTimer = 0f;
            worker.AwakeTimer = 0f;
            worker.WakeUpTimer = 0f;

            var workerView = _workerViewRepository.GetWorkerViewById(worker.Id);
            workerView.SetSleeping(false, _workerVisualConfiguration);
            workerView.PlayWakeUpEffect(_workerVisualConfiguration);

            _signalBus.Fire(new WorkerStateChangedSignal
            {
                WorkerId = worker.Id,
                State = WorkerState.Awake
            });
        }

        public void HandleTriggerStayPublic(WorkerView workerView, Collider other)
        {
            HandleTriggerStay(workerView, other);
        }

        public void HandleTriggerExitPublic(WorkerView workerView, Collider other)
        {
            HandleTriggerExit(workerView, other);
        }

        private void HandleTriggerStay(WorkerView workerView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            var worker = _workerRepository.GetWorkerById(workerView.Id);

            if (worker.State != WorkerState.Sleeping)
                return;

            worker.WakeUpTimer += Time.deltaTime;

            if (worker.WakeUpTimer >= worker.WakeUpDurationSeconds)
            {
                WakeUpWorker(worker);
            }
        }

        private void HandleTriggerExit(WorkerView workerView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            var worker = _workerRepository.GetWorkerById(workerView.Id);
            worker.WakeUpTimer = 0f;
        }
    }
}
