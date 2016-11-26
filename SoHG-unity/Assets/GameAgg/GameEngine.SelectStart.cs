using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace Sohg.GameAgg
{
    public partial class GameEngine : ISelectStartPlayable
    {
        public void CreateSocieties(ICell humanInitialCell)
        {
            societies = new List<ISociety>();

            CreateSociety(gameDefinition.PlayerSociety, humanInitialCell);
            for (var i = 0; i < sohgFactory.Config.NonPlayerSocietyCount; i++)
            {
                CreateSociety(gameDefinition.NonPlayerSociety);
            }

            grid.ExpandSocietiesTerritories();
        }
        
        private void CreateSociety(ISocietyDefinition societyDefinition, params ICell[] initialCells)
        {
            var newSociety = sohgFactory.CreateSociety(societyDefinition, initialCells);
            societies.Add(newSociety);
        }
    }
}
