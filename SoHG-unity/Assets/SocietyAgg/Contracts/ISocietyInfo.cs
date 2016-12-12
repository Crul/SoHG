using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyInfo
    {
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
