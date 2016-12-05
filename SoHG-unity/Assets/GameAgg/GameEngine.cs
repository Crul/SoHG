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
        private SohgFactoryScript sohgFactory;

        private IEndGame endGame;
        private IGrid grid;
        private IInstructions instructions;
        private ISocietyInfo societyInfo;

        public IGameDefinition GameDefinition { get; private set; }
        public List<ISociety> Societies { get; private set; }

        public ISociety PlayerSociety { get; private set; }
        public int FaithPower { get; private set; }
        public int TotalFaith { get; private set; }

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

            grid = SohgFactory.CreateGrid();
            grid.AddOnCellClick(cell => OnGridCellClick(cell)); // TODO fix non-blocking grid click

            gameInfoPanel.TechnologyPanel.Initialize(this);
        }
    }
}
