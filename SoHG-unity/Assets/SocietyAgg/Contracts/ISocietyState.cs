namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        float FriendshipRange { get; }
        int MaximumAttacks { get; }
        float Power { get; }

        void Kill(float damageRate);
        void SetInitialPopulation(long initialPopulation);
    }
}
