using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg;
using Sohg.CrossCutting;
using UnityEngine;
using Sohg.SpeciesAgg.Contracts;

namespace Sohg.SocietyAgg
{
    [CreateAssetMenu(fileName = "NewSociety", menuName = "SoHG/Society Definition")]
    public class SocietyDefinitionScript : ScriptableBaseObject, ISocietyDefinition
    {
        [SerializeField]
        private string societyName;
        [SerializeField]
        private SpeciesDefinitionScript species;

        public string Name { get { return societyName; } }
        public ISpeciesDefinition Species { get { return species; } }
    }
}
