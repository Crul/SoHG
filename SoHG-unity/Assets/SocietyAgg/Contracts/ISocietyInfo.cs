﻿using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyInfo
    {
        GameObject ActionsPanel { get; }
        GameObject EffectsPanel { get; }
        GameObject PropertiesPanel { get; }

        IRunningGame Game { get; }
        ISociety Society { get; }

        void Initialize(IRunningGame game);
        void Hide();
        void Reset();
        void Show(ISociety society);
    }
}
