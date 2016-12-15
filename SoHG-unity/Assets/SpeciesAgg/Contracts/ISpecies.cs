using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;
using Sohg.GameAgg.Contracts;

namespace Sohg.SpeciesAgg.Contracts
{
    public interface ISpecies
    {
        string Name { get; }
        Color Color { get; }
        List<ISociety> Societies { get; }

        float InitialAggressivityRate { get; }
        int InitialPopulationDensity { get; }
        float InitialTechnologyLevelRate { get; }

        int FaithPower { get; }
        int TotalFaith { get; }

        void Reset();
        void Evolve(IEvolvableGame game);

        void AddFaith(int faithAmount);
        bool ConsumeFaith(int faithAmount);
    }
}
