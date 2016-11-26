using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.GameAgg
{
    public partial class GameEngine : ISelectStartPlayable
    {
        public void CreateSocieties(ICell humanInitialCell)
        {
            Societies = new List<ISociety>();

            CreateSociety(gameDefinition.PlayerSociety, humanInitialCell);
            for (var i = 0; i < SohgFactory.Config.NonPlayerSocietyCount; i++)
            {
                CreateSociety(gameDefinition.NonPlayerSociety);
            }

            grid.ExpandSocietiesTerritories();

            Societies.ForEach(society => society.Initialize());
        }
        
        private void CreateSociety(ISocietyDefinition societyDefinition, params ICell[] initialCells)
        {
            var newSociety = SohgFactory.CreateSociety(this, societyDefinition, initialCells);
            Societies.Add(newSociety);
        }
    }
}
