using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        public void AddFaith(int faithAmount)
        {
            FaithPower += faithAmount;
            TotalFaith += faithAmount;
        }

        public bool ConsumeFaith(int faithAmount)
        {
            var isEnoughFaith = (faithAmount <= FaithPower);
            if (isEnoughFaith)
            {
                FaithPower -= faithAmount;
            }

            return isEnoughFaith;
        }

        public void EmitFaith(ISociety society)
        {
            var faithAmount = society.State.GetFaithEmitted();
            if (faithAmount == 0)
            {
                return;
            }

            var faithCell = grid
                .GetRandomCell(cell => cell.TerritoryIndex == society.Territory.TerritoryIndex);

            SohgFactory.CreateFaith(this, faithCell, faithAmount);
        }
    }
}
