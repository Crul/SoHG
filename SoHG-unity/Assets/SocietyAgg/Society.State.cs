using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg
{
    public partial class Society
    {
        public ISocietyState State { get; private set; }
        
        public void Initialize()
        {
            var initialPopulation = Territory.CellCount * config.InitialSocietyPopulationByCell;
            State.SetInitialPopulation(initialPopulation);
        }
    }
}
