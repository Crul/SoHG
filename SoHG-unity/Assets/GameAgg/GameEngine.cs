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

namespace Sohg.GameAgg
{
    [DisallowMultipleComponent]
    public partial class GameEngine : BaseComponent
    {
        [SerializeField]
        private Canvas boardCanvas;
        [SerializeField]
        private Canvas boardOverCanvas;
        [SerializeField]
        private Canvas fixedOverCanvas;
        [SerializeField]
        private GameInfoPanel gameInfoPanel;
        [SerializeField]
        private SohgFactory sohgFactory;

        private IEndGame endGame;
        private IInstructions instructions;
        private ISocietyInfo societyInfo;

        public IGameDefinition GameDefinition { get; private set; }
        public IGrid Grid { get; private set; }
        public ISpecies PlayerSpecies { get; private set; }
        public List<ISpecies> Species { get; private set; }
        public List<ISociety> Societies { get; private set; }
        public int Year { get; set; }

        public IGameInfoPanel GameInfoPanel { get { return gameInfoPanel; } }
        public ISohgFactory SohgFactory { get { return sohgFactory; } }

        public void Awake()
        {
            SohgFactory.SetCanvas(boardCanvas, boardOverCanvas, fixedOverCanvas);
            
            GameDefinition = SohgFactory.GameDefinition;

            // TODO remove single-instance-using unity prefabs from prefabFactory
            endGame = SohgFactory.CreateEndGame();
            instructions = SohgFactory.CreateInstructions();
            societyInfo = SohgFactory.CreateSocietyInfo(this);
            Grid = SohgFactory.CreateGrid();

            Grid.AddOnCellClick(cell => OnGridCellClick(cell));
            Grid.AddOnTerritoryClick(territory => OnGridTerritoryClick(territory));

            gameInfoPanel.GameStatusInfo.SetGame(this);
            gameInfoPanel.TechnologyPanel.Initialize(this, GameDefinition.TechnologyCategories.ToList());

            PlayerSpecies = GameDefinition.PlayerSpecies;
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
