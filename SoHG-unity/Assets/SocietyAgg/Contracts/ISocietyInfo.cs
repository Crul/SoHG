using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyInfo : IPointerEnterHandler, IPointerExitHandler
    {
        bool IsVisible { get; }
        GameObject ActionsPanel { get; }
        GameObject EffectsPanel { get; }
        GameObject PropertiesPanel { get; }
        GameObject SkillsPanel { get; }

        IRunningGame Game { get; }
        ISociety Society { get; }

        void Initialize(IRunningGame game);
        void Hide();
        void Refresh();
        void Reset();
        void Show(ISociety society);
    }
}
