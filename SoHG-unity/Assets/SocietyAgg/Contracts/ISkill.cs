using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISkill : ITechnologyDependent
    {
        string Name { get; }
        Sprite SkillIcon { get; }
        float FaithShrinkingRateBonus { get; }
        float SeaMovementCapacityBonus { get; }
        float TechnologyRateBonus { get; }
    }
}
