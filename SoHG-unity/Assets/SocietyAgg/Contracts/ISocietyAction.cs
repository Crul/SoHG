using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyAction : ITechnologyDependent
    {
        Sprite ActionIcon { get; }
        int FaithCost { get; }
        
        void Execute(ISociety society);
        bool IsActionEnabled(ISociety society);
    }
}
