using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg;
using System.Linq;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "SoHG/Skill")]
    public class Skill : TechnologyDependent, ISkill
    {
        [SerializeField]
        private Sprite skillIcon;
        [SerializeField]
        private float technologyRateBonus;
        [SerializeField]
        private float faithShrinkingRateBonus;
        [SerializeField]
        private float seaMovementCapacityBonus;

        public Sprite SkillIcon { get { return skillIcon; } }
        public float FaithShrinkingRateBonus { get { return faithShrinkingRateBonus; } }
        public float SeaMovementCapacityBonus { get { return seaMovementCapacityBonus; } }
        public float TechnologyRateBonus { get { return technologyRateBonus; } }

        protected override void Activate()
        {
            game.PlayerSpecies.Societies
                .OrderByDescending(society => society.State.TechnologyLevelRate)
                .First()
                .AddSkill(this);
        }
    }
}
