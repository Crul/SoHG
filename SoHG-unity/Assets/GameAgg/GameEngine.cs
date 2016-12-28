using Grids2D;
using Sohg.CrossCutting;
using Sohg.CrossCutting.Contracts;
using Sohg.CrossCutting.Factories;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg.UI;
using Sohg.SpeciesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg
{
    [DisallowMultipleComponent]
    public partial class GameEngine : BaseComponent
    {
        [SerializeField]
        private SohgFactory sohgFactory;
        
        [SerializeField]
        private Canvas boardOverCanvas;

        [SerializeField]
        private EndGame endGame;
        [SerializeField]
        private GameInfoPanel gameInfoPanel;
        [SerializeField]
        private Grid2D grid;
        [SerializeField]
        private Instructions instructions;
        [SerializeField]
        private SocietyInfo societyInfo;

        private IEndGame EndGame { get { return endGame; } }
        private IInstructions Instructions { get { return instructions; } }
        private ISocietyInfo SocietyInfo { get { return societyInfo; } }

        public List<ISpecies> Species { get; private set; }
        public List<ISociety> Societies { get; private set; }
        public int Year { get; set; }

        public Canvas BoardOverCanvas { get { return boardOverCanvas; } }
        public IGameDefinition GameDefinition { get { return SohgFactory.GameDefinition; } }
        public IGameInfoPanel GameInfoPanel { get { return gameInfoPanel; } }
        public IGrid Grid { get { return grid; } }
        public ISpecies PlayerSpecies { get { return GameDefinition.PlayerSpecies; } }
        public ISohgFactory SohgFactory { get { return sohgFactory; } }

        public void Awake()
        {
            SohgFactory.SetGame(this);
            
            Grid.AddOnCellClick(cell => OnGridCellClick(cell));
            Grid.AddOnTerritoryClick(territory => OnGridTerritoryClick(territory));

            SocietyInfo.Initialize(this);
            GameInfoPanel.GameStatusInfo.SetGame(this);
            GameInfoPanel.TechnologyPanel.Initialize(this, GameDefinition.TechnologyCategories.ToList());
        }

        public void ResetGame()
        {
            hasPlayerWon = null;

            var nonPlayerSpeciesCount = Math.Min(GameDefinition.NonPlayerSocietyCount, GameDefinition.NonPlayerSpecies.Length);
            Species = GameDefinition.NonPlayerSpecies.Take(nonPlayerSpeciesCount).ToList();
            Species.Add(PlayerSpecies);

            Species.ForEach(species => species.Reset());

            Societies = new List<ISociety>();

            GameDefinition.TechnologyCategories
                .SelectMany(technologyCategory => technologyCategory.Technologies)
                .ToList()
                .ForEach(technology => technology.Reset());

            GameDefinition.SocietyActions.ToList()
                .ForEach(action => action.Initialize(this));

            GameDefinition.Skills.ToList()
                .ForEach(skill => skill.Initialize(this));

            Year = 0;
        }
    }
}
