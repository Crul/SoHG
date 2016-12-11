using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship : IRelationship
    {
        public ISociety We { get; private set; }
        public ISociety Them { get; private set; }

        private float friendshipRange;

        public Relationship(ISociety we, ISociety them)
        {
            We = we;
            Them = them;
            friendshipRange = 0.5f; // TODO update Relationship.firendshipRange
        }

        public bool AreWeNeighbours()
        {
            var frontierCells = We.Territory.FrontierCellsByTerritoryIndex[Them.Territory.TerritoryIndex];

            return frontierCells != null && frontierCells.Count > 0;
        }
    }
}
