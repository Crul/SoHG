using Sohg.CrossCutting;
using Sohg.CrossCutting.Contracts;
using Sohg.CrossCutting.Factories;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sohg.GameAgg
{
    [DisallowMultipleComponent]
    public partial class GameEngine : BaseComponent
    {
        [SerializeField]
        private SohgFactoryScript sohgFactory;
        [SerializeField]
        private Canvas boardCanvas;

        public List<ISociety> Societies { get; private set; }
        public ISohgFactory SohgFactory { get { return sohgFactory; } }

        private IGameDefinition gameDefinition;
        private IGrid grid;
        private ISocietyInfo societyInfo;
        
        public void Awake()
        {
            SohgFactory.SetCanvas(boardCanvas);

            gameDefinition = SohgFactory.GameDefinition;

            societyInfo = SohgFactory.CreateSocietyInfo(this);

            grid = SohgFactory.GetGrid();
            grid.AddOnCellClick(cell => OnGridCellClick(cell)); // TODO fix non-blocking grid click
        }
    }
}
