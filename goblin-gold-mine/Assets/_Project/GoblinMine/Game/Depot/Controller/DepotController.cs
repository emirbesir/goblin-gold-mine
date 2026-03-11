using System;
using _Project.GoblinMine.Game.Depot.Command;
using _Project.GoblinMine.Game.Depot.Signal;
using _Project.GoblinMine.Game.Depot.View;
using _Project.GoblinMine.Game.Haptic.Command;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.Inventory.View;
using _Project.GoblinMine.Game.Player.View;
using _Project.Shared.Initializable;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Depot.Controller
{
    public class DepotController : IPreInitializable, IPostInitializable, IDisposable
    {
        private readonly DepotView _depotView;
        private readonly DepositResourcesCommand _depositResourcesCommand;
        private readonly InventoryRepository _inventoryRepository;
        private readonly InventoryViewRepository _inventoryViewRepository;
        private readonly TriggerHapticCommand _triggerHapticCommand;
        private readonly SignalBus _signalBus;

        private float _depositCooldownTimer;
        private const float DepositCooldownSeconds = 0.5f;

        public DepotController(
            DepotView depotView,
            DepositResourcesCommand depositResourcesCommand,
            InventoryRepository inventoryRepository,
            InventoryViewRepository inventoryViewRepository,
            TriggerHapticCommand triggerHapticCommand,
            SignalBus signalBus)
        {
            _depotView = depotView;
            _depositResourcesCommand = depositResourcesCommand;
            _inventoryRepository = inventoryRepository;
            _inventoryViewRepository = inventoryViewRepository;
            _triggerHapticCommand = triggerHapticCommand;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            _depotView.OnTriggerStayAction += HandleTriggerStay;
            _depotView.OnTriggerExitAction += HandleTriggerExit;
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<ResourcesDepositedSignal>(OnResourcesDeposited);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResourcesDepositedSignal>(OnResourcesDeposited);
        }

        private void HandleTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            _depositCooldownTimer += Time.deltaTime;

            if (_depositCooldownTimer >= DepositCooldownSeconds)
            {
                _depositCooldownTimer = 0f;

                var totalCarried = _inventoryRepository.GetTotalCarried();
                if (totalCarried <= 0)
                    return;

                _depositResourcesCommand.Execute();
                _depotView.PlayDepositEffect();
                _triggerHapticCommand.Execute();

                UpdateInventoryUI();
            }
        }

        private void HandleTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            _depositCooldownTimer = 0f;
        }

        private void OnResourcesDeposited(ResourcesDepositedSignal signal)
        {
            UpdateInventoryUI();
        }

        private void UpdateInventoryUI()
        {
            foreach (var resource in _inventoryRepository.Resources)
            {
                var resourceView = _inventoryViewRepository.GetResourceViewById(resource.Id);
                resourceView.SetText(resource.Amount.ToString());
            }
        }
    }
}
