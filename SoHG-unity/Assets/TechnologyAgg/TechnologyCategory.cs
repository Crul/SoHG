using Sohg.CrossCutting;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.TechnologyAgg
{
    [CreateAssetMenu(fileName = "TechnologyCategory", menuName = "SoHG/Technologies/Category", order = 0)]
    public class TechnologyCategory : ScriptableBaseObject, ITechnologyCategory
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private Color color;
        [SerializeField]
        private Technology[] technologies;

        public string Name { get { return name; } }
        public Color Color { get { return color; } }
        public ITechnology[] Technologies { get { return technologies; } }
    }
}
