using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameFeature
    {
        void Run(IEvolvableGame game, ISociety society);
    }
}
