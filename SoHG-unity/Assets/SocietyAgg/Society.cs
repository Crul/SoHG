using System.Collections.Generic;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting.Contracts;

namespace Sohg.SocietyAgg
{
    public partial class Society : ISociety
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public ITerritory Territory { get; private set; }

        private List<IRelationship> relationships;
        private ISohgFactory sohgFactory;

        private ISohgConfig config { get { return sohgFactory.Config; } }

        public Society(ISohgFactory sohgFactory, ISocietyDefinition societyDefinition, ITerritory territory)
        {
            relationships = new List<IRelationship>();
            this.sohgFactory = sohgFactory;

            Name = societyDefinition.Name;
            Color = societyDefinition.Color;
            State = new SocietyState(societyDefinition);
            Territory = territory;
        }

        public void AddRelationship(ISociety otherSociety)
        {
            var newRelationship = sohgFactory.CreateRelationship(this, otherSociety);
            relationships.Add(newRelationship);
        }

        public void SetNeighbours(List<ISociety> neighbourSocieties)
        {
            relationships
                .ForEach(relationship => relationship.SetNeighbour(neighbourSocieties));
        }
    }
}
