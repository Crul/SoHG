using UnityEngine;

namespace Sohg.GameAgg.Contracts
{
    public interface ITechnology
    {
        string Name { get; }
        int FaithCost { get; }
        Sprite TechnologyIcon { get; }
    }
}
