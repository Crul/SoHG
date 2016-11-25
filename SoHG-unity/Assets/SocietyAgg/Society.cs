using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg
{
    public class Society : ISociety
    {
        private ISocietyDefinition societyDefinition;
        private ITerritory territory;

        public string Name { get { return societyDefinition.Name; } }

        public Society(ISocietyDefinition societyDefinition, ITerritory territory)
        {
            this.societyDefinition = societyDefinition;
            this.territory = territory;
        }
    }
}
