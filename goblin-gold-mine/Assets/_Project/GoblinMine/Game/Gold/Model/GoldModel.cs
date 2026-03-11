using System;

namespace _Project.GoblinMine.Game.Gold.Model
{
    public class GoldModel
    {
        public int Amount { get; set; }
        public Action<int> OnAmountChanged { get; set; }
    }
}
