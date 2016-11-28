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
        private Grid2D gridPrefab;
        [SerializeField]
        private SocietyMarker societyMarkerPrefab;
        [SerializeField]
        private SocietyInfo societyInfoPrefab;
        [SerializeField]
        private SocietyActionButton societyActionButtonPrefab;
        [SerializeField]
        private SocietyEffectIcon societyEffectIconPrefab;
        [SerializeField]
        private SocietyPropertyInfo societyPropertyInfoPrefab;
        [SerializeField]
        private Fight fightPrefab;

        public IGrid InstantiateGrid(Canvas canvas)
        {
            return InstantiateIntoCanvas(gridPrefab, canvas, "Grid2D");
        }

        public ISocietyMarker InstantiateSocietyMarker(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(societyMarkerPrefab, canvas, name);
        }

        public IFight InstantiateFight(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(fightPrefab, canvas, name);
        }

        public ISocietyInfo InstantiateSocietyInfo(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(societyInfoPrefab, canvas, name);
        }

        public ISocietyActionButton InstantiateSocietyActionButton(GameObject gameObject, string name)
        {
            return InstantiateInto(societyActionButtonPrefab, gameObject, name);
        }

        public ISocietyEffectIcon InstantiateSocietyEffectIcon(GameObject gameObject, string name)
        {
            return InstantiateInto(societyEffectIconPrefab, gameObject, name);
        }

        public ISocietyPropertyInfo InstantiateSocietyPropertyInfo(GameObject gameObject, string name)
        {
            return InstantiateInto(societyPropertyInfoPrefab, gameObject, name);
        }

        private T InstantiateIntoCanvas<T>(T original, Canvas canvas, string name = "")
            where T : Component
        {
            return InstantiateInto(original, canvas.gameObject, name);
        }

        private T InstantiateInto<T>(T original, GameObject gameObject, string name = "")
            where T : Component
        {
            var newGameObject = (T)Instantiate(original, gameObject.transform);
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