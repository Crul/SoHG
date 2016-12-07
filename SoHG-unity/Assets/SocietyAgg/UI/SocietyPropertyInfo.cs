using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting.UI;

namespace Sohg.SocietyAgg.UI
{
    public enum SocietyProperty
    {
        None,
        Population,
        Power
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
                case SocietyProperty.Power: return "Power";
            }

            return string.Format("ERROR - Invalid society property: {0}", societyProperty.ToString());
        }

        private string GetValue()
        {
            switch (societyProperty)
            {
                case SocietyProperty.Population:
                    return society.State.PopulationAmount.ToString("### ### ### ### ### ##0"); // TODO number format

                case SocietyProperty.Power:
                    return society.State.Power.ToString();
            }

            return string.Format("ERROR - Invalid society property: {0}", societyProperty.ToString());
        }
    }
}
