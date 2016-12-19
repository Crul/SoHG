using Sohg.Grids2D.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISociety
    {
        string Name { get; }
        ISpecies Species { get; }
        ISocietyState State { get; }
        ITerritory Territory { get; }
        Color Color { get; }
        
        bool IsDead { get; }
        Dictionary<ISocietyAction, bool> IsEffectActive { get; }
        IEnumerable<ISocietyAction> Actions { get; }
        IEnumerable<IRelationship> Relationships { get; }
        IEnumerable<ISkill> Skills { get; }

        void AddAction(ISocietyAction societyAction);
        void AddRelationship(ISociety society);
        void AddSkill(ISkill skill);
        void RemoveRelationship(ISociety deathSociety);
        
        void Initialize();
        bool IsNeighbourOf(ISociety society);
    }
}
