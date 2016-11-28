using System.Collections.Generic;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship : IRelationship
    {
        public bool IsNeighbour { get; private set; }
        public ISociety We { get; private set; }
        public ISociety Them { get; private set; }

        private float friendshipRange;

        public Relationship(ISociety we, ISociety them)
        {
            We = we;
            Them = them;
            friendshipRange = 0.5f; // TODO update Relationship.firendshipRange
        }

        public void SetNeighbour(List<ISociety> neighbourSocieties)
        {
            IsNeighbour = neighbourSocieties.Contains(Them);
        }
    }
}
