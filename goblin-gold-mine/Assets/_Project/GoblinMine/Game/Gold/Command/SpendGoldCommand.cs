using _Project.GoblinMine.Game.Gold.Repository;
using _Project.GoblinMine.Game.Gold.Signal;
using Zenject;

namespace _Project.GoblinMine.Game.Gold.Command
{
    public class SpendGoldCommand
    {
        private readonly GoldRepository _goldRepository;
        private readonly SignalBus _signalBus;

        public SpendGoldCommand(GoldRepository goldRepository, SignalBus signalBus)
        {
            _goldRepository = goldRepository;
            _signalBus = signalBus;
        }

        public bool Execute(int amount)
        {
            if (_goldRepository.Gold.Amount < amount)
                return false;

            _goldRepository.Gold.Amount -= amount;
            _goldRepository.Gold.OnAmountChanged?.Invoke(_goldRepository.Gold.Amount);

            _signalBus.Fire(new GoldChangedSignal
            {
                NewAmount = _goldRepository.Gold.Amount,
                Delta = -amount
            });

            return true;
        }
    }
}
