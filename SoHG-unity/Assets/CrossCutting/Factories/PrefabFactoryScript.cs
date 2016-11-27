using Sohg.SocietyAgg.UI;
using Grids2D;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "PrefabFactory", menuName = "SoHG/Prefab Factory")]
    public class PrefabFactoryScript : ScriptableBaseObject
    {
        // TODO instantiate prefabs into "folder objects" to avoid canvas-children-hell

        [SerializeField]
        private Grid2D GridPrefab;
        [SerializeField]
        private SocietyMarker SocietyMarkerPrefab;
        [SerializeField]
        private Fight FightPrefab;

        public IGrid InstantiateGrid(Canvas canvas)
        {
            return InstantiateInto(GridPrefab, canvas, "Grid2D");
        }

        public ISocietyMarker InstantiateSocietyMarker(Canvas canvas, string name)
        {
            return InstantiateInto(SocietyMarkerPrefab, canvas, name);
        }

        public IFight InstantiateFight(Canvas canvas, string name)
        {
            return InstantiateInto(FightPrefab, canvas, name);
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