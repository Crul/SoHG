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

        private IGameDefinition gameDefinition;
        private IEndGame endGame;
        private IGrid grid;
        private IInstructions instructions;
        private ISocietyInfo societyInfo;

        public ISpecies PlayerSpecies { get; private set; }
        public List<ISpecies> Species { get; private set; }

        public IGameInfoPanel GameInfoPanel { get { return gameInfoPanel; } }
        public ISohgFactory SohgFactory { get { return sohgFactory; } }

        public void Awake()
        {
            SohgFactory.SetCanvas(boardCanvas, boardOverCanvas, fixedOverCanvas);
            
            gameDefinition = SohgFactory.GameDefinition;

            // TODO remove single-instance-using unity prefabs from prefabFactory
            endGame = SohgFactory.CreateEndGame();
            instructions = SohgFactory.CreateInstructions();
            societyInfo = SohgFactory.CreateSocietyInfo(this);
            grid = SohgFactory.CreateGrid();

            grid.AddOnCellClick(cell => OnGridCellClick(cell)); // TODO fix non-blocking grid click

            gameInfoPanel.TechnologyPanel.Initialize(this, gameDefinition.TechnologyCategories.ToList());

            PlayerSpecies = gameDefinition.PlayerSpecies;
        }

        public void ResetGame()
        {
            hasPlayerWon = null;

            var nonPlayerSpeciesCount = Math.Min(SohgFactory.Config.NonPlayerSocietyCount, gameDefinition.NonPlayerSpecies.Length);
            Species = gameDefinition.NonPlayerSpecies.Take(nonPlayerSpeciesCount).ToList();
            Species.Add(PlayerSpecies);

            Species.ForEach(species => species.Reset());

            gameDefinition.TechnologyCategories
                .SelectMany(technologyCategory => technologyCategory.Technologies)
                .ToList()
                .ForEach(technology => technology.Reset());

            gameDefinition.SocietyActions.ToList()
                .ForEach(action => action.Initialize(this));

            gameDefinition.Skills.ToList()
                .ForEach(skill => skill.Initialize(this));
        }
    }
}
