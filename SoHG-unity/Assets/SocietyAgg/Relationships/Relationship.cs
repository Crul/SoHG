using System.Collections.Generic;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship : IRelationship
    {
        public bool IsNeighbour { get; private set; }
        public ISociety We { get; private set; }
        public ISociety Them { get; private set; }

        public Relationship(ISociety we, ISociety them)
        {
            We = we;
            Them = them;
        }

        public void SetNeighbour(List<ISociety> neighbourSocieties)
        {
            IsNeighbour = neighbourSocieties.Contains(Them);
        }
    }
}
