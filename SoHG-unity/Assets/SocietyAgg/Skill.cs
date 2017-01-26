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
        private string name;
        [SerializeField]
        private Sprite skillIcon;
        [SerializeField]
        private float technologyRateBonus;
        [SerializeField]
        private float faithShrinkingRateBonus;
        [SerializeField]
        private float seaMovementCapacityBonus;

        public string Name { get { return name; } }
        public Sprite SkillIcon { get { return skillIcon; } }
        public float FaithShrinkingRateBonus { get { return faithShrinkingRateBonus; } }
        public float SeaMovementCapacityBonus { get { return seaMovementCapacityBonus; } }
        public float TechnologyRateBonus { get { return technologyRateBonus; } }

        protected override void Activate()
        {
            var society = game.PlayerSpecies.Societies
                .OrderByDescending(playerSociety => playerSociety.State.TechnologyLevelRate)
                .First();

            society.AddSkill(this);
            game.OnSkillActivated(this, society);
        }
    }
}
