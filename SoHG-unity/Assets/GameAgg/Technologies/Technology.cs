using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Technologies
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
