﻿using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sohg.CrossCutting.Factories;

namespace Sohg.SocietyAgg
{
    public partial class Society : ISociety
    {
        private List<ISocietyAction> actions;
        private List<IRelationship> relationships;
        private List<ISkill> skills;
        private ISohgFactory sohgFactory;
        
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public ISpecies Species { get; private set; }
        public ISocietyState State { get; private set; }
        public ITerritory Territory { get; private set; }
        
        public Dictionary<ISocietyAction, bool> IsEffectActive { get; private set; }

        public int TerritoryExtension { get { return Territory.CellCount; } }
        public bool IsDead { get { return State.Population == 0 || Territory.CellCount == 0; } }
        public IEnumerable<ISocietyAction> Actions { get { return actions; } }
        public IEnumerable<IRelationship> Relationships { get { return relationships; } }
        public IEnumerable<ISkill> Skills { get { return skills; } }

        public Society(ISohgFactory sohgFactory, ISpecies species, ITerritory territory)
        {
            actions = new List<ISocietyAction>();
            relationships = new List<IRelationship>();
            skills = new List<ISkill>();
            IsEffectActive = new Dictionary<ISocietyAction, bool>();

            Name = species.GetNextSocietyName();
            Color = species.Color;
            this.sohgFactory = sohgFactory;
            Species = species;
            State = new SocietyState(this);
            Territory = territory;
        }

        public Society(SohgFactory sohgFactory, ISociety originSociety, ITerritory territory)
            : this(sohgFactory, originSociety.Species, territory)
        {
            actions = originSociety.Actions.ToList();
            IsEffectActive = originSociety.IsEffectActive.ToDictionary(xx => xx.Key, xx => false);
            skills = originSociety.Skills.ToList();

            State.InheritState(originSociety);
        }

        public void AddAction(ISocietyAction societyAction)
        {
            IsEffectActive.Add(societyAction, false);
            actions.Add(societyAction);
        }

        public void AddRelationship(IRelationship relationship)
        {
            relationships.Add(relationship);
        }

        public void AddSkill(ISkill skill)
        {
            State.OnSkillAdded(skill);
            skills.Add(skill);
        }

        public bool IsNeighbourOf(ISociety society)
        {
            var societyRelationship = relationships.FirstOrDefault(relationship => relationship.Them == society);

            return societyRelationship.AreWeNeighbours();
        }

        public IRelationship GetRelationship(ISociety society)
        {
            return Relationships.Single(relationship => relationship.Them == society);
        }

        public void RemoveRelationship(ISociety society)
        {
            var relationshipToRemove = relationships.FirstOrDefault(relationship => relationship.Them == society);
            if (relationshipToRemove != null)
            {
                relationships.Remove(relationshipToRemove);
            }
        }
    }
}
