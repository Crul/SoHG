using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Sohg.SocietyAgg.UI
{
    public class SocietySkillIcon : SocietyInfoChild, ISocietySkillIcon
    {
        [SerializeField]
        private Image iconImage;

        private ISkill skill;

        public void Initialize(ISkill skill, ISocietyInfo societyInfo)
        {
            Initialize(societyInfo);
            this.skill = skill;

            iconImage.sprite = skill.SkillIcon;
        }

        public void Update()
        {
            var isSkillActive = (society.Skills.Contains(skill));
            gameObject.SetActive(isSkillActive);
        }
    }
}
