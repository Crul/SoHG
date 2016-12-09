﻿using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
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
        
        Dictionary<ISocietyAction, bool> IsEffectActive { get; }
        IEnumerable<ISocietyAction> Actions { get; }
        IEnumerable<ISkill> Skills { get; }

        void AddAction(ISocietyAction societyAction);
        void AddRelationship(ISociety society);
        void AddSkill(ISkill skill);
        void RemoveRelationship(ISociety deathSociety);

        bool IsNeighbourOf(ISociety society);
        void SetNeighbours(List<ISociety> neighbourSocieties);

        void Initialize();
        void Evolve(IWarPlayable game);
        bool ShouldWeAttack(ISociety them);
    }
}
