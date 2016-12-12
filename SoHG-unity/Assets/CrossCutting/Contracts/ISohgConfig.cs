namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgConfig
    {
        int NonPlayerSocietyCount { get; }
        long InitialPopulationByCell { get; }
        int InitialSocietyPopulationLimit { get; }

        int EvolutionActionsTimeInterval { get; }

        float AttackDamageTieRateThreshold { get; }
        int FightDuration { get; }
        float FriendshipRangeBottomThresholdForAttack { get; }
        float PowerBalanceThresholdForAttack { get; }
    }
}
