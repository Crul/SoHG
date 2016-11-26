namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        float FriendshipRange { get; }
        float Power { get; }

        void SetInitialPopulation(long initialPopulation);
    }
}
