using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg;
using System.Linq;
using UnityEngine;
using System;

namespace Sohg.SocietyAgg.Skills
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "SoHG/Skill")]
    public class Skill : TechnologyDependent, ISkill
    {
        [SerializeField]
        private Sprite skillIcon;
        [SerializeField]
        private float technologyRateBonus;

        public Sprite SkillIcon { get { return skillIcon; } }
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
