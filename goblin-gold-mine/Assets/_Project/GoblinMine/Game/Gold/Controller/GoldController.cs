using System;
using _Project.GoblinMine.Game.Gold.Command;
using _Project.GoblinMine.Game.Gold.Repository;
using _Project.GoblinMine.Game.Gold.Signal;
using _Project.GoblinMine.Game.Gold.View;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Signal;
using _Project.Shared.Initializable;
using Zenject;

namespace _Project.GoblinMine.Game.Gold.Controller
{
    public class GoldController : IPreInitializable, IPostInitializable, IDisposable
    {
        private readonly GoldRepository _goldRepository;
        private readonly GoldView _goldView;
        private readonly CreateGoldModelCommand _createGoldModelCommand;
        private readonly EarnGoldCommand _earnGoldCommand;
        private readonly MiningResourceConfigurationCollection _configCollection;
        private readonly SignalBus _signalBus;

        public GoldController(
            GoldRepository goldRepository,
            GoldView goldView,
            CreateGoldModelCommand createGoldModelCommand,
            EarnGoldCommand earnGoldCommand,
            MiningResourceConfigurationCollection configCollection,
            SignalBus signalBus)
        {
            _goldRepository = goldRepository;
            _goldView = goldView;
            _createGoldModelCommand = createGoldModelCommand;
            _earnGoldCommand = earnGoldCommand;
            _configCollection = configCollection;
            _signalBus = signalBus;
        }

        public void PreInitialize()
        {
            var gold = _createGoldModelCommand.Execute();
            _goldRepository.Gold = gold;
            _goldView.SetGoldAmount(0);
        }

        public void PostInitialize()
        {
            _signalBus.Subscribe<GoldChangedSignal>(OnGoldChanged);
            _signalBus.Subscribe<ResourceCollectedSignal>(OnResourceCollected);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GoldChangedSignal>(OnGoldChanged);
            _signalBus.Unsubscribe<ResourceCollectedSignal>(OnResourceCollected);
        }

        private void OnGoldChanged(GoldChangedSignal signal)
        {
            _goldView.SetGoldAmount(signal.NewAmount);
        }

        private void OnResourceCollected(ResourceCollectedSignal signal)
        {
            if (!signal.AutoDeposit)
                return;

            var config = _configCollection.GetConfigurationByType(signal.ResourceType);
            var goldEarned = signal.CollectionAmount * config.EconomicValue;
            _earnGoldCommand.Execute(goldEarned);
        }
    }
}
