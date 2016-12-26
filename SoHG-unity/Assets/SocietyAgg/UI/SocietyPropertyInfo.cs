using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting.UI;

namespace Sohg.SocietyAgg.UI
{
    public enum SocietyProperty
    {
        None,
        Population,
        PopulationDensity,
        Power,
        Production,
        Resources,
        SpeciesName,
        Technology
    };

    [DisallowMultipleComponent]
    public class SocietyPropertyInfo : ValueInfo, ISocietyPropertyInfo
    {
        private ISocietyInfo societyInfo;
        private SocietyProperty societyProperty;

        private ISociety society { get { return societyInfo.Society; } }

        public void Initialize(SocietyProperty societyProperty, ISocietyInfo societyInfo)
        {
            this.societyInfo = societyInfo;
            this.societyProperty = societyProperty;

            SetTitle(GetTitle());
        }
        
        public void Update()
        {
            if (gameObject.activeSelf && society != null)
            {
                SetValue(GetValue());
            }
        }
        
        private string GetTitle()
        {
            switch (societyProperty)
            {
                case SocietyProperty.Population: return "Population";
                case SocietyProperty.PopulationDensity: return "Pop. Density";
                case SocietyProperty.Power: return "Power";
                case SocietyProperty.Production: return "Production";
                case SocietyProperty.Resources: return "Resources";
                case SocietyProperty.SpeciesName: return "Species";
                case SocietyProperty.Technology: return "Technology";
            }

            return string.Format("ERROR - Invalid society property: {0}", societyProperty.ToString());
        }

        private string GetValue()
        {
            switch (societyProperty)
            {
                case SocietyProperty.Population:
                    return society.State.Population.ToString("### ### ### ### ### ##0"); // TODO number format

                case SocietyProperty.PopulationDensity:
                    return society.State.PopulationDensity.ToString("### ### ### ### ### ##0"); // TODO number format

                case SocietyProperty.Power:
                    return society.State.Power.ToString("### ### ##0"); // TODO number format

                case SocietyProperty.Production:
                    return society.State.Production.ToString("### ### ##0"); // TODO number format

                case SocietyProperty.Resources:
                    return society.State.Resources.ToString("### ### ##0"); // TODO number format

                case SocietyProperty.SpeciesName:
                    return society.Species.Name;

                case SocietyProperty.Technology:
                    return society.State.TechnologyLevelRate.ToString("##.# %"); // TODO number format
            }

            return string.Format("ERROR - Invalid society property: {0}", societyProperty.ToString());
        }
    }
}
