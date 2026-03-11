using System;
using System.Collections.Generic;
using _Project.GoblinMine.Game.Haptic.Command;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.Region.Command;
using _Project.GoblinMine.Game.Region.Configuration;
using _Project.GoblinMine.Game.Region.Model;
using _Project.GoblinMine.Game.Region.Repository;
using _Project.GoblinMine.Game.Region.Signal;
using _Project.GoblinMine.Game.Region.View;
using _Project.Shared.Initializable;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Region.Controller
{
    public class RegionController : IPreInitializable, IPostInitializable, IDisposable
    {
        private readonly RegionRepository _regionRepository;
        private readonly RegionConfiguration _regionConfiguration;
        private readonly List<RegionGateView> _regionGateViews;
        private readonly UnlockRegionCommand _unlockRegionCommand;
        private readonly TriggerHapticCommand _triggerHapticCommand;
        private readonly SignalBus _signalBus;

        public RegionController(
            RegionRepository regionRepository,
            RegionConfiguration regionConfiguration,
            List<RegionGateView> regionGateViews,
            UnlockRegionCommand unlockRegionCommand,
            TriggerHapticCommand triggerHapticCommand,
            SignalBus signalBus)
        {
            _regionRepository = regionRepository;
            _regionConfiguration = regionConfiguration;
            _regionGateViews = regionGateViews;
            _unlockRegionCommand = unlockRegionCommand;
            _triggerHapticCommand = triggerHapticCommand;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            for (var i = 0; i < _regionConfiguration.Regions.Count; i++)
            {
                var entry = _regionConfiguration.Regions[i];
                var region = new RegionModel
                {
                    RegionIndex = i,
                    IsUnlocked = false,
                    UnlockCost = entry.unlockCost
                };
                _regionRepository.Regions.Add(region);
            }

            foreach (var gateView in _regionGateViews)
            {
                var region = _regionRepository.GetRegionByIndex(gateView.RegionIndex);
                if (region != null)
                {
                    gateView.SetLocked(true);
                    gateView.OnTriggerEnterAction += other => HandleGateTrigger(gateView, other);
                }
            }
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<RegionUnlockedSignal>(OnRegionUnlocked);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RegionUnlockedSignal>(OnRegionUnlocked);
        }

        private void HandleGateTrigger(RegionGateView gateView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _))
                return;

            var region = _regionRepository.GetRegionByIndex(gateView.RegionIndex);
            if (region == null || region.IsUnlocked)
                return;

            if (_unlockRegionCommand.Execute(gateView.RegionIndex))
            {
                _triggerHapticCommand.Execute();
            }
        }

        private void OnRegionUnlocked(RegionUnlockedSignal signal)
        {
            foreach (var gateView in _regionGateViews)
            {
                if (gateView.RegionIndex == signal.RegionIndex)
                {
                    gateView.PlayUnlockEffect();
                    break;
                }
            }
        }
    }
}
