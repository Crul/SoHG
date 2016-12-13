using Sohg.GameAgg.Contracts;
using UnityEngine.EventSystems;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyMarker : IPointerEnterHandler, IPointerExitHandler
    {
        void Initialize(IRunningGame game, ISociety society);
    }
}
