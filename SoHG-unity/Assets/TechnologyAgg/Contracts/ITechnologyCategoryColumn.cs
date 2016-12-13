using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyCategoryColumn
    {
        GameObject Content { get; }

        void Initialize(ITechnologyCategory technologyCategory);
        void SetState(IRunningGame game);
    }
}
