using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyAction
    {
        Sprite ActionIcon { get; }

        void Initialize(IRunningGame game);
        void Execute(ISociety society);
    }
}
