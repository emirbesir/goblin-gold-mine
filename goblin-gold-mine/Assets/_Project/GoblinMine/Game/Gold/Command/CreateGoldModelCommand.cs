using _Project.GoblinMine.Game.Gold.Model;

namespace _Project.GoblinMine.Game.Gold.Command
{
    public class CreateGoldModelCommand
    {
        public GoldModel Execute()
        {
            return new GoldModel
            {
                Amount = 0
            };
        }
    }
}
