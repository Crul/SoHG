﻿namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgConfig
    {
        int NonPlayerSocietyCount { get; }
        long InitialSocietyPopulationByCell { get; }

        int WarActionsTimeInterval { get; }

        float AttackDamageTieRateThreshold { get; }
        int FightDuration { get; }
        float FriendshipRangeBottomThresholdForAttack { get; }
        float PowerBalanceThresholdForAttack { get; }
    }
}