using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        public ITerritory Territory { get; private set; }
        
        public Dictionary<ISocietyAction, bool> IsEffectActive { get; private set; }

        private ISohgConfig config { get { return sohgFactory.Config; } }

        public IEnumerable<ISocietyAction> Actions { get { return actions; } }
        public IEnumerable<ISkill> Skills { get { return skills; } }

        public Society(ISohgFactory sohgFactory, ISpecies species, ITerritory territory)
        {
            actions = new List<ISocietyAction>();
            relationships = new List<IRelationship>();
            skills = new List<ISkill>();
            IsEffectActive = new Dictionary<ISocietyAction, bool>();

            Name = species.Name;
            Color = species.Color;
            this.sohgFactory = sohgFactory;
            Species = species;
            State = new SocietyState(this);
            Territory = territory;
        }

        public void AddAction(ISocietyAction societyAction)
        {
            IsEffectActive.Add(societyAction, false);
            actions.Add(societyAction);
        }

        public void AddRelationship(ISociety otherSociety)
        {
            var newRelationship = sohgFactory.CreateRelationship(this, otherSociety);
            relationships.Add(newRelationship);
        }

        public void AddSkill(ISkill skill)
        {
            State.OnSkillAdded(skill);
            skills.Add(skill);
        }

        public void RemoveRelationship(ISociety society)
        {
            var relationshipToRemove = relationships.Single(relationship => relationship.Them == society);
            relationships.Remove(relationshipToRemove);
        }

        public bool IsNeighbourOf(ISociety society)
        {
            return relationships.Single(relationship => relationship.Them == society).AreWeNeighbours();
        }
    }
}
