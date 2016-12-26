using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D
    {
        public void OnTerritorySplit(ITerritory territory1, ITerritory territory2)
        {
            territories.ForEach(territory => territory.InitializeFrontier(this));

            ((Territory)territory1).cells.ToList()
                .ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));
            ((Territory)territory2).cells.ToList()
                .ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));

            FixNonInvadableTerritories();

            territoriesHaveChanged = true;
        }

        public List<ICell> SplitTerritory(ITerritory territory)
        {
            var newTerritoryCellIndices = territory.FrontierCellIndices
                .OrderBy(cellIndex => Random.Range(0f, 1f))
                .Take(1)
                .ToList();

            var newTerritorySeedCell = GetCell(newTerritoryCellIndices.Single());

            var oldTerritoryCellIndices = territory.FrontierCellIndices
                .OrderByDescending(cellIndex => Vector3.Distance(GetCell(cellIndex).WorldPosition, newTerritorySeedCell.WorldPosition))
                .Take(1)
                .ToList();

            var territoryCells = ((Territory)territory).cells;
            var nonAssignedCellIndices = territoryCells
                .Where(cell => cell != newTerritorySeedCell && !oldTerritoryCellIndices.Contains(cell.CellIndex))
                .Select(cell => cell.CellIndex)
                .ToList();
            
            while (nonAssignedCellIndices.Count > 0)
            {
                var oldTerritoryCellIndicesToAdd = oldTerritoryCellIndices
                    .SelectMany(cell => CellGetNeighbours(cell)
                        .Select(neighbour => neighbour.CellIndex)
                        .Where(neighbourIndex => nonAssignedCellIndices.Contains(neighbourIndex)))
                    .Distinct()
                    .ToList();

                oldTerritoryCellIndices.AddRange(oldTerritoryCellIndicesToAdd);
                oldTerritoryCellIndicesToAdd.ForEach(cellIndex => nonAssignedCellIndices.Remove(cellIndex));

                var newTerritoryCellIndicesToAdd = newTerritoryCellIndices
                    .SelectMany(cell => CellGetNeighbours(cell)
                        .Select(neighbour => neighbour.CellIndex)
                        .Where(neighbourIndex => nonAssignedCellIndices.Contains(neighbourIndex)))
                    .Distinct()
                    .ToList();
                
                newTerritoryCellIndices.AddRange(newTerritoryCellIndicesToAdd);
                newTerritoryCellIndicesToAdd.ForEach(cellIndex => nonAssignedCellIndices.Remove(cellIndex));
            }
            
            newTerritoryCellIndices.ForEach(cellIndex => territoryCells.Remove(cells[cellIndex]));

            return newTerritoryCellIndices.Select(cellIndex => GetCell(cellIndex)).ToList();
        }
    }
}
