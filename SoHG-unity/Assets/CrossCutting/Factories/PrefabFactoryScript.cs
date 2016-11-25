using Grids2D;
using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "PrefabFactory", menuName = "SoHG/Prefab Factory")]
    public class PrefabFactoryScript : ScriptableBaseObject
    {
        public Grid2D GridPrefab;

        public IGrid InstantiateGrid(Canvas canvas)
        {
            return InstantiateInto(GridPrefab, canvas, "Grid2D");
        }

        private T InstantiateInto<T>(T original, Canvas canvas, string name = "")
            where T : Component
        {
            var newGameObject = (T)Instantiate(original, canvas.transform);
            if (!string.IsNullOrEmpty(name))
            {
                newGameObject.name = name;
            }
            else
            {
                newGameObject.name = original.name;
            }

            return newGameObject;
        }
    }
}