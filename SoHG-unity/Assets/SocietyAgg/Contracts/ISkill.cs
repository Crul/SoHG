using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISkill : ITechnologyDependent
    {
        Sprite SkillIcon { get; }
        float TechnologyRateBonus { get; }
    }
}
