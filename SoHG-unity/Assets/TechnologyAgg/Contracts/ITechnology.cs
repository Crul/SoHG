using UnityEngine;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnology
    {
        string Name { get; }
        int FaithCost { get; }
        Sprite TechnologyIcon { get; }
    }
}
