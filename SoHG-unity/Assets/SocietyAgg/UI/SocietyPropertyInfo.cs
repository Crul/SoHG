using Sohg.SocietyAgg.Contracts;
using Sohg.CrossCutting;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    public enum SocietyProperty
    {
        None,
        Population,
        Power
    };

    [DisallowMultipleComponent]
    public class SocietyPropertyInfo : BaseComponent, ISocietyPropertyInfo
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text valueText;

        private SocietyProperty societyProperty;
        private ISociety society;
        
        public void Initialize(SocietyProperty societyProperty)
        {
            this.societyProperty = societyProperty;
            titleText.text = GetTitle();
        }

        public void SetSociety(ISociety society)
        {
            this.society = society;
        }

        public void Update()
        {
            if (gameObject.activeSelf && society != null)
            {
                valueText.text = GetValue(society);
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

        private string GetValue(ISociety society)
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
