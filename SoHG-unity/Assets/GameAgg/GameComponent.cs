using Sohg.CrossCutting;
using Sohg.CrossCutting.Factories;
using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg
{
    public class GameComponent : BaseComponent
    {
        public SohgFactoryScript sohgFactory;
        public Canvas BoardCanvas;

        private IRunnableGame gameEngine;

        public void Awake()
        {
            sohgFactory.SetCanvas(BoardCanvas);
        }
        
        public void Start()
        {
            gameEngine = sohgFactory.CreateGameEngine();
            gameEngine.Start();
        }

        public void FixedUpdate()
        {
            gameEngine.FixedUpdate();
        }
    }
}
