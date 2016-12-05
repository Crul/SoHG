using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyInfo
    {
        GameObject ActionsPanel { get; }
        GameObject EffectsPanel { get; }
        GameObject PropertiesPanel { get; }
        ISociety Society { get; }

        void AddAction(ISocietyAction action);
        void Initialize(IRunningGame game);
        void Hide();
        void Show(ISociety society);
    }
}
