using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyAction
    {
        Sprite ActionIcon { get; }
        int FaithCost { get; }

        void Initialize(IRunningGame game);
        void Execute(ISociety society);
        bool Requires(ITechnology technology);
    }
}
