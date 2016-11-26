using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    public class Society : ISociety
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public ITerritory Territory { get; private set; }

        public Society(ISocietyDefinition societyDefinition, ITerritory territory)
        {
            Name = societyDefinition.Name;
            Color = societyDefinition.Color;
            Territory = territory;
        }
    }
}
