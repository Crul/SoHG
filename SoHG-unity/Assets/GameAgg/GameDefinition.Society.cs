using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Actions;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg;
using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameDefinition
    {
        [Header("Society")]
        [SerializeField]
        private Species playerSpecies;
        [SerializeField]
        private Species[] nonPlayerSpecies;
        [SerializeField]
        private int nonPlayerSocietyCount;
        [SerializeField]
        private int initialSocietyPopulationLimit;

        [Space]
        [SerializeField]
        private Skill[] skills;
        [SerializeField]
        private SocietyAction[] societyActions;

        public ISpecies PlayerSpecies { get { return playerSpecies; } }
        public ISpecies[] NonPlayerSpecies { get { return nonPlayerSpecies; } }
        public int NonPlayerSocietyCount { get { return nonPlayerSocietyCount; } }
        public int InitialSocietyPopulationLimit { get { return initialSocietyPopulationLimit; } }

        public ISkill[] Skills { get { return skills; } }
        public ISocietyAction[] SocietyActions { get { return societyActions; } }
    }
}
