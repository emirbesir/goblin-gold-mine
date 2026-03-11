using System;
using _Project.GoblinMine.Game.Gold.Repository;
using _Project.GoblinMine.Game.Gold.Signal;
using _Project.GoblinMine.Game.Haptic.Command;
using _Project.GoblinMine.Game.Player.Model;
using _Project.GoblinMine.Game.Player.Repository;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.Upgrade.Command;
using _Project.GoblinMine.Game.Upgrade.Configuration;
using _Project.GoblinMine.Game.Upgrade.Model;
using _Project.GoblinMine.Game.Upgrade.Repository;
using _Project.GoblinMine.Game.Upgrade.Signal;
using _Project.GoblinMine.Game.Upgrade.View;
using _Project.GoblinMine.Game.Worker.Command;
using _Project.GoblinMine.Game.Worker.Controller;
using _Project.Shared.Initializable;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Upgrade.Controller
{
    public class UpgradeController : IPreInitializable, IPostInitializable, IDisposable
    {
        private readonly UpgradeRepository _upgradeRepository;
        private readonly UpgradeConfiguration _upgradeConfiguration;
        private readonly UpgradeStationView _upgradeStationView;
        private readonly UpgradePanelView _upgradePanelView;
        private readonly GoldRepository _goldRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly CreateUpgradeModelsCommand _createUpgradeModelsCommand;
        private readonly PurchaseUpgradeCommand _purchaseUpgradeCommand;
        private readonly SpawnWorkerCommand _spawnWorkerCommand;
        private readonly TriggerHapticCommand _triggerHapticCommand;
        private readonly WorkerController _workerController;
        private readonly SignalBus _signalBus;

        private bool _isPlayerNearStation;

        public UpgradeController(
            UpgradeRepository upgradeRepository,
            UpgradeConfiguration upgradeConfiguration,
            UpgradeStationView upgradeStationView,
            UpgradePanelView upgradePanelView,
            GoldRepository goldRepository,
            PlayerRepository playerRepository,
            CreateUpgradeModelsCommand createUpgradeModelsCommand,
            PurchaseUpgradeCommand purchaseUpgradeCommand,
            SpawnWorkerCommand spawnWorkerCommand,
            TriggerHapticCommand triggerHapticCommand,
            WorkerController workerController,
            SignalBus signalBus)
        {
            _upgradeRepository = upgradeRepository;
            _upgradeConfiguration = upgradeConfiguration;
            _upgradeStationView = upgradeStationView;
            _upgradePanelView = upgradePanelView;
            _goldRepository = goldRepository;
            _playerRepository = playerRepository;
            _createUpgradeModelsCommand = createUpgradeModelsCommand;
            _purchaseUpgradeCommand = purchaseUpgradeCommand;
            _spawnWorkerCommand = spawnWorkerCommand;
            _triggerHapticCommand = triggerHapticCommand;
            _workerController = workerController;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            var upgrades = _createUpgradeModelsCommand.Execute();
            _upgradeRepository.Upgrades = upgrades;

            _upgradeStationView.OnTriggerEnterAction += HandleTriggerEnter;
            _upgradeStationView.OnTriggerExitAction += HandleTriggerExit;

            foreach (var optionView in _upgradePanelView.UpgradeOptionViews)
            {
                optionView.OnBuyClicked += HandleBuyClicked;
            }

            _upgradePanelView.SetVisible(false);
            InitializeOptionViews();
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<UpgradePurchasedSignal>(OnUpgradePurchased);
            _signalBus.Subscribe<GoldChangedSignal>(OnGoldChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<UpgradePurchasedSignal>(OnUpgradePurchased);
            _signalBus.Unsubscribe<GoldChangedSignal>(OnGoldChanged);
        }

        private void InitializeOptionViews()
        {
            foreach (var optionView in _upgradePanelView.UpgradeOptionViews)
            {
                var upgrade = _upgradeRepository.GetUpgradeByType(optionView.UpgradeType);
                optionView.SetName(GetUpgradeDisplayName(optionView.UpgradeType));
                optionView.SetCost(upgrade.CurrentCost);
                optionView.SetLevel(upgrade.Level);
            }
        }

        private void HandleTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            _isPlayerNearStation = true;
            _upgradePanelView.SetVisible(true);
            RefreshButtonStates();
        }

        private void HandleTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            _isPlayerNearStation = false;
            _upgradePanelView.SetVisible(false);
        }

        private void HandleBuyClicked(UpgradeType type)
        {
            if (_purchaseUpgradeCommand.Execute(type))
            {
                ApplyUpgrade(type);
                _triggerHapticCommand.Execute();
            }
        }

        private void ApplyUpgrade(UpgradeType type)
        {
            var player = _playerRepository.Player;

            switch (type)
            {
                case UpgradeType.BuyWorker:
                    _spawnWorkerCommand.Execute(
                        _workerController.HandleTriggerStayPublic,
                        _workerController.HandleTriggerExitPublic);
                    break;

                case UpgradeType.MiningSpeed:
                    var newInterval = player.BaseMiningIntervalSeconds -
                                     _upgradeConfiguration.MiningSpeedReductionPerLevel *
                                     _upgradeRepository.GetUpgradeByType(type).Level;
                    player.MiningIntervalSeconds = Mathf.Max(
                        newInterval,
                        _upgradeConfiguration.MiningSpeedMinIntervalSeconds);
                    break;

                case UpgradeType.MoveSpeed:
                    var newSpeed = player.BaseMoveSpeedUnitsPerSecond +
                                  _upgradeConfiguration.MoveSpeedIncreasePerLevel *
                                  _upgradeRepository.GetUpgradeByType(type).Level;
                    player.MoveSpeedUnitsPerSecond = Mathf.Min(
                        newSpeed,
                        _upgradeConfiguration.MoveSpeedMaxUnitsPerSecond);
                    break;

                case UpgradeType.CarryCapacity:
                    player.MaxCarryCapacity += _upgradeConfiguration.CarryCapacityIncreasePerLevel;
                    break;
            }
        }

        private void OnUpgradePurchased(UpgradePurchasedSignal signal)
        {
            var optionView = _upgradePanelView.GetOptionByType(signal.UpgradeType);
            if (optionView == null) return;

            var upgrade = _upgradeRepository.GetUpgradeByType(signal.UpgradeType);
            optionView.SetCost(upgrade.CurrentCost);
            optionView.SetLevel(upgrade.Level);
            RefreshButtonStates();
        }

        private void OnGoldChanged(GoldChangedSignal signal)
        {
            if (_isPlayerNearStation)
                RefreshButtonStates();
        }

        private void RefreshButtonStates()
        {
            var gold = _goldRepository.Gold.Amount;

            foreach (var optionView in _upgradePanelView.UpgradeOptionViews)
            {
                var upgrade = _upgradeRepository.GetUpgradeByType(optionView.UpgradeType);
                optionView.SetInteractable(gold >= upgrade.CurrentCost);
            }
        }

        private string GetUpgradeDisplayName(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.BuyWorker: return "New Worker";
                case UpgradeType.MiningSpeed: return "Mining Speed";
                case UpgradeType.MoveSpeed: return "Move Speed";
                case UpgradeType.CarryCapacity: return "Carry Capacity";
                default: return type.ToString();
            }
        }
    }
}
