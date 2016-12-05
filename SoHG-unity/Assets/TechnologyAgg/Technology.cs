using Sohg.CrossCutting;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.TechnologyAgg
{
    public abstract class Technology : ScriptableBaseObject, ITechnology
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int faithCost;
        [SerializeField]
        private Sprite technologyIcon;

        public string Name { get { return name; } }
        public int FaithCost { get { return faithCost; } }
        public Sprite TechnologyIcon { get { return technologyIcon; } }
    }
}
