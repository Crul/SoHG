using Sohg.GameAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyPanel
    {
        void Initialize(IRunningGame game, List<ITechnologyCategory> technologyCategories);
        bool IsVisible();
        void Open();
    }
}
