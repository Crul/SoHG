using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg;
using Sohg.CrossCutting;
using UnityEngine;
using Sohg.SpeciesAgg.Contracts;
using System;

namespace Sohg.SocietyAgg
{
    [CreateAssetMenu(fileName = "NewSociety", menuName = "SoHG/Society Definition")]
    public class SocietyDefinitionScript : ScriptableBaseObject, ISocietyDefinition
    {
        [SerializeField]
        private string societyName;
        [SerializeField]
        private SpeciesDefinitionScript species;
        [SerializeField]
        private Color color;
        [SerializeField]
        private float initialAggressivityRate;
        [SerializeField]
        private float initialTechnologyLevelRate;

        public string Name { get { return societyName; } }
        public ISpeciesDefinition Species { get { return species; } }
        public Color Color { get { return color; } }

        public float InitialAggressivityRate { get { return initialAggressivityRate; } }
        public float InitialTechnologyLevelRate { get { return initialTechnologyLevelRate; } }
    }
}
