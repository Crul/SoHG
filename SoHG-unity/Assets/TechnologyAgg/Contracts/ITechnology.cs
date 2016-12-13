using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnology
    {
        string Name { get; }
        int FaithCost { get; }
        Sprite TechnologyIcon { get; }
        bool IsActive { get; }

        bool Activate(IEvolvableGame game);
        void Reset();
    }
}
