using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Features
{
    public abstract class GameFeature : ScriptableBaseObject, IGameFeature
    {
        public abstract void Run(IEvolvableGame game, ISociety society);
    }
}
