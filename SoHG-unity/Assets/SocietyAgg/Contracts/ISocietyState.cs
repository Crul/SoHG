namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        int MaximumAttacks { get; }
        long PopulationAmount { get; }
        float Power { get; }

        void Kill(float damageRate);
        void SetInitialPopulation(long initialPopulation);
    }
}
