namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgConfig
    {
        int NonPlayerSocietyCount { get; }
        long InitialSocietyPopulationByCell { get; }

        float FriendshipRangeBottomThresholdForAttack { get; }
        float PowerBalanceThresholdForAttack { get; }
    }
}
