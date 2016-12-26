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
        [SerializeField]
        private string[] societyNames;

        public string Name { get { return speciesName; } }
        public Color Color { get { return speciesColor; } }
        public float InitialAggressivityRate { get { return initialAggressivityRate; } }
        public int InitialPopulationDensity { get { return initialPopulationByCell; } }
        public float InitialTechnologyLevelRate { get { return initialTechnologyLevelRate; } }
        public string NextSocietyName
        {
            get
            {
                if (societyNames.Length == 0)
                {
                    return Name;
                }

                var societyNameIndex = (Societies.Count % (societyNames.Length - 1));
                var societyBaseName = societyNames[societyNameIndex];
                var societyPrefix = string.Empty;

                var societyNameRepetitionCounter = (Societies.Count / (societyNames.Length - 1));
                switch (societyNameRepetitionCounter)
                {
                    case 0:break;
                    case 1:
                        societyPrefix = "Late ";
                        break;
                    case 2:
                        societyPrefix = "Post ";
                        break;
                    default:
                        societyPrefix = string.Format("Post ({0})", societyNameRepetitionCounter - 2);
                        break;
                }

                return societyPrefix + societyBaseName;
            }
        }

        public List<ISociety> Societies { get; private set; }
        public int FaithPower { get; private set; }
        public int TotalFaith { get; private set; }

        public Species()
        {
            Reset();
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

        public void Evolve(IEvolvableGame game)
        {
        }

        public void Reset()
        {
            Societies = new List<ISociety>();
            FaithPower = 0;
            TotalFaith = 0;
        }
    }
}
