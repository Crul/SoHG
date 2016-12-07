using System.Collections.Generic;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using Sohg.GameAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using Sohg.SocietyAgg.Actions;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISociety
    {
        string Name { get; }
        ISpecies Species { get; }
        ISocietyState State { get; }
        ITerritory Territory { get; }
        Color Color { get; }
        
        Dictionary<ISocietyAction, bool> IsEffectActive { get; }
        IEnumerable<ISocietyAction> Actions { get; }

        void AddAction(SocietyAction societyAction);
        void AddRelationship(ISociety society);
        void RemoveRelationship(ISociety deathSociety);

        bool IsNeighbourOf(ISociety society);
        void SetNeighbours(List<ISociety> neighbourSocieties);

        void Initialize();
        void Evolve(IWarPlayable game);
        bool ShouldWeAttack(ISociety them);
    }
}
