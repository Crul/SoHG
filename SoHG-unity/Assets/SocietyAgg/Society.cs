using System.Collections.Generic;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting.Contracts;
using System.Linq;

namespace Sohg.SocietyAgg
{
    public partial class Society : ISociety
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public ITerritory Territory { get; private set; }

        public Dictionary<ISocietyAction, bool> IsActionEnabled { get; private set; }
        public Dictionary<ISocietyAction, bool> IsEffectActive { get; private set; }

        private List<IRelationship> relationships;
        private ISohgFactory sohgFactory;

        private ISohgConfig config { get { return sohgFactory.Config; } }

        public Society(ISohgFactory sohgFactory,
            ISocietyDefinition societyDefinition, ITerritory territory)
        {
            relationships = new List<IRelationship>();
            IsActionEnabled = new Dictionary<ISocietyAction, bool>();
            IsEffectActive = new Dictionary<ISocietyAction, bool>();

            Name = societyDefinition.Name;
            Color = societyDefinition.Color;
            this.sohgFactory = sohgFactory;
            State = new SocietyState(societyDefinition);
            Territory = territory;

            sohgFactory.GameDefinition.SocietyActions
                .ToList().ForEach(action => InitializeAction(action));
        }

        private void InitializeAction(ISocietyAction action)
        {
            IsActionEnabled.Add(action, true); // TODO ! configure actions based on ??
            IsEffectActive.Add(action, false);
        }

        public void AddRelationship(ISociety otherSociety)
        {
            var newRelationship = sohgFactory.CreateRelationship(this, otherSociety);
            relationships.Add(newRelationship);
        }

        public void RemoveRelationship(ISociety society)
        {
            var relationshipToRemove = relationships.Single(relationship => relationship.Them == society);
            relationships.Remove(relationshipToRemove);
        }

        public void SetNeighbours(List<ISociety> neighbourSocieties)
        {
            relationships
                .ForEach(relationship => relationship.SetNeighbour(neighbourSocieties));
        }
    }
}
