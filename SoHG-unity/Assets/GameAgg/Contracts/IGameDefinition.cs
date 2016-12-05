﻿using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameStage[] Stages { get; }
        ISocietyAction[] SocietyActions { get; }
        ITechnology[] Technologies { get; }

        ISocietyDefinition PlayerSociety { get; }
        ISocietyDefinition NonPlayerSociety { get; }
    }
}
