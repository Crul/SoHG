using System;
using Sohg.CrossCutting;
using Sohg.CrossCutting.Factories;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting.Contracts;
using System.Collections.Generic;
using Sohg.SpeciesAgg.Contracts;
using System.Linq;
using Grids2D;
using Sohg.SocietyAgg.UI;

namespace Sohg.GameAgg
{
    [DisallowMultipleComponent]
    public partial class GameEngine : BaseComponent
    {
        [SerializeField]
        private SohgFactory sohgFactory;

        [SerializeField]
        private Canvas boardCanvas;
        [SerializeField]
        private Canvas boardOverCanvas;
        [SerializeField]
        private Canvas fixedOverCanvas;

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

        public IGrid Grid { get { return grid; } }
        private IEndGame EndGame { get { return endGame; } }
        private IInstructions Instructions { get { return instructions; } }
        private ISocietyInfo SocietyInfo { get { return societyInfo; } }

        public List<ISpecies> Species { get; private set; }
        public List<ISociety> Societies { get; private set; }
        public int Year { get; set; }

        public IGameDefinition GameDefinition { get { return SohgFactory.GameDefinition; } }
        public IGameInfoPanel GameInfoPanel { get { return gameInfoPanel; } }
        public ISpecies PlayerSpecies { get { return GameDefinition.PlayerSpecies; } }
        public ISohgFactory SohgFactory { get { return sohgFactory; } }

        public void Awake()
        {
            SohgFactory.SetCanvas(boardCanvas, boardOverCanvas, fixedOverCanvas);
            
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
