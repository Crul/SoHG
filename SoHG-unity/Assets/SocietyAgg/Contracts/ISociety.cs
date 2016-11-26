using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISociety
    {
        string Name { get; }
        ITerritory Territory { get; }
        Color Color { get; }
    }
}
