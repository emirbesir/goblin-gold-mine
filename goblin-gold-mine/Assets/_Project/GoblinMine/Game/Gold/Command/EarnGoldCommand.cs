using _Project.GoblinMine.Game.Gold.Repository;
using _Project.GoblinMine.Game.Gold.Signal;
using Zenject;

namespace _Project.GoblinMine.Game.Gold.Command
{
    public class EarnGoldCommand
    {
        private readonly GoldRepository _goldRepository;
        private readonly SignalBus _signalBus;

        public EarnGoldCommand(GoldRepository goldRepository, SignalBus signalBus)
        {
            _goldRepository = goldRepository;
            _signalBus = signalBus;
        }

        public void Execute(int amount)
        {
            _goldRepository.Gold.Amount += amount;
            _goldRepository.Gold.OnAmountChanged?.Invoke(_goldRepository.Gold.Amount);

            _signalBus.Fire(new GoldChangedSignal
            {
                NewAmount = _goldRepository.Gold.Amount,
                Delta = amount
            });
        }
    }
}
