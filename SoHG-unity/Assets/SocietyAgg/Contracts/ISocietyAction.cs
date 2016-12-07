using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyAction
    {
        Sprite ActionIcon { get; }
        int FaithCost { get; }
        bool IsActive { get; }

        void CheckActivation(IRunningGame game);
        void Execute(ISociety society);
        void Initialize(IRunningGame game);
        bool IsActionEnabled(ISociety society);
    }
}
