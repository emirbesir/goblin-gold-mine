using _Project.GoblinMine.Game.Gold.Command;
using _Project.GoblinMine.Game.Region.Repository;
using _Project.GoblinMine.Game.Region.Signal;
using Zenject;

namespace _Project.GoblinMine.Game.Region.Command
{
    public class UnlockRegionCommand
    {
        private readonly RegionRepository _regionRepository;
        private readonly SpendGoldCommand _spendGoldCommand;
        private readonly SignalBus _signalBus;

        public UnlockRegionCommand(
            RegionRepository regionRepository,
            SpendGoldCommand spendGoldCommand,
            SignalBus signalBus)
        {
            _regionRepository = regionRepository;
            _spendGoldCommand = spendGoldCommand;
            _signalBus = signalBus;
        }

        public bool Execute(int regionIndex)
        {
            var region = _regionRepository.GetRegionByIndex(regionIndex);

            if (region == null || region.IsUnlocked)
                return false;

            if (!_spendGoldCommand.Execute(region.UnlockCost))
                return false;

            region.IsUnlocked = true;

            _signalBus.Fire(new RegionUnlockedSignal
            {
                RegionIndex = regionIndex
            });

            return true;
        }
    }
}
