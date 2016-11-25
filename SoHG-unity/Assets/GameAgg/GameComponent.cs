using Sohg.CrossCutting;
using Sohg.CrossCutting.Factories;
using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Sohg.GameAgg
{
    public class GameComponent : BaseComponent
    {
        public SohgFactoryScript sohgFactory;
        public Canvas BoardCanvas;
        
        private IGrid grid;

        public void Start()
        {
            grid = sohgFactory.CreateGrid(BoardCanvas);
        }
    }
}
