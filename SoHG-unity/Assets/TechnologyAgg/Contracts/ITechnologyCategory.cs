using UnityEngine;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyCategory
    {
        string Name { get; }
        Color Color { get; }
        ITechnology[] Technologies { get; }
    }
}
