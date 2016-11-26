using Sohg.CrossCutting;
using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.SpeciesAgg
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "SoHG/Species Definition")]
    public class SpeciesDefinitionScript : ScriptableBaseObject, ISpeciesDefinition
    {
        [SerializeField]
        private string speciesNme;

        public string Name { get { return speciesNme; } }
    }
}
