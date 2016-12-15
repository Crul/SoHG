using System;
using System.Collections.Generic;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.SpeciesAgg
{
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "SoHG/Species")]
    public class Species : ScriptableBaseObject, ISpecies
    {
        [SerializeField]
        private string speciesName;
        [SerializeField]
        private Color speciesColor;
        [SerializeField]
        private float initialAggressivityRate;
        [SerializeField]
        private int initialPopulationByCell;
        [SerializeField]
        private float initialTechnologyLevelRate;

        public string Name { get { return speciesName; } }
        public Color Color { get { return speciesColor; } }
        public float InitialAggressivityRate { get { return initialAggressivityRate; } }
        public int InitialPopulationDensity { get { return initialPopulationByCell; } }
        public float InitialTechnologyLevelRate { get { return initialTechnologyLevelRate; } }

        public List<ISociety> Societies { get; private set; }
        public int FaithPower { get; private set; }
        public int TotalFaith { get; private set; }

        public Species()
        {
            Reset();
        }

        public void Reset()
        {
            Societies = new List<ISociety>();
            FaithPower = 0;
            TotalFaith = 0;
        }

        public void Evolve(IEvolvableGame game)
        {
        }

        public void AddFaith(int faithAmount)
        {
            FaithPower += faithAmount;
            TotalFaith += faithAmount;
        }

        public bool ConsumeFaith(int faithAmount)
        {
            var isEnoughFaith = (faithAmount <= FaithPower);
            if (isEnoughFaith)
            {
                FaithPower -= faithAmount;
            }

            return isEnoughFaith;
        }
    }
}
