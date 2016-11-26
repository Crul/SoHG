using Sohg.Grids2D.Contracts;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg.Stages
{
    [System.Serializable]
    public class GameStageScript : ScriptableBaseObject, IGameStage
    {
        private IRunningGame _game;

        public void SetGame(IRunningGame game)
        {
            _game = game;
        }

        protected TRunnableGame GetGame<TRunnableGame>()
            where TRunnableGame : IRunningGame
        {
            return (TRunnableGame)_game;
        }

        public virtual void Start() { }
        public virtual void FixedUpdate() { }
        public virtual void OnCellClick(ICell cell) { }
    }
}
