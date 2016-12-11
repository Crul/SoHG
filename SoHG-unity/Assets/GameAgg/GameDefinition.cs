using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Stages;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.SocietyAgg.Actions;
using Sohg.TechnologyAgg;
using Sohg.TechnologyAgg.Contracts;
using Sohg.SpeciesAgg;
using Sohg.SpeciesAgg.Contracts;
using Sohg.SocietyAgg;

namespace Sohg.GameAgg
{
    [CreateAssetMenu(fileName = "GameDefinition", menuName = "SoHG/Game Definition")]
    public class GameDefinition : ScriptableBaseObject, IGameDefinition
    {
        [SerializeField]
        private GameStage[] stages;
        [SerializeField]
        private TechnologyCategory[] technologyCategories;

        [Header("Society")]
        [SerializeField]
        private Species playerSpecies;
        [SerializeField]
        private Species[] nonPlayerSpecies;
        [Space]
        [SerializeField]
        private Skill[] skills;
        [SerializeField]
        private SocietyAction[] societyActions;

        public IGameStage[] Stages { get { return stages; } }
        public ITechnologyCategory[] TechnologyCategories { get { return technologyCategories; } }
        public ISpecies PlayerSpecies { get { return playerSpecies; } }
        public ISpecies[] NonPlayerSpecies { get { return nonPlayerSpecies; } }
        public ISkill[] Skills { get { return skills; } }
        public ISocietyAction[] SocietyActions { get { return societyActions; } }
    }
}
