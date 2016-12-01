using Sohg.SocietyAgg.UI;
using Grids2D;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using Sohg.SocietyAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.GameAgg.Contracts;
using Sohg.CrossCutting.Pooling;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "PrefabFactory", menuName = "SoHG/Prefab Factory")]
    public class PrefabFactoryScript : ScriptableBaseObject
    {
        // TODO instantiate prefabs into "folder objects" to avoid canvas-children-hell

        [SerializeField]
        private Grid2D gridPrefab;
        [SerializeField]
        private EndGame endGamePrefab;
        [SerializeField]
        private FaithRecolectable faithRecolectablePrefab;
        [SerializeField]
        private Instructions instructionsPrefab;
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

        public IFaithRecolectable InstantiateFaithRecolectable(Canvas canvas, string name)
        {
            return InstantiatePooledIntoCanvas(faithRecolectablePrefab, canvas, name);
        }

        public IEndGame InstantiateEndGame(Canvas canvas)
        {
            return InstantiateIntoCanvas(endGamePrefab, canvas, "EndGame");
        }

        public IInstructions InstantiateInstructions(Canvas canvas)
        {
            return InstantiateIntoCanvas(instructionsPrefab, canvas, "Instructions");
        }

        public ISocietyMarker InstantiateSocietyMarker(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(societyMarkerPrefab, canvas, name);
        }

        public IFight InstantiateFight(Canvas canvas, string name)
        {
            return InstantiatePooledIntoCanvas(fightPrefab, canvas, name);
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

        public T InstantiatePooledIntoCanvas<T>(T prefab, Canvas canvas, string name = "")
            where T : PooledObject
        {
            var pooledObject = prefab.GetPooledInstance<T>(canvas);
            pooledObject.name = name;

            return pooledObject;
        }

        private T InstantiateIntoCanvas<T>(T prefab, Canvas canvas, string name = "")
            where T : Component
        {
            return InstantiateInto(prefab, canvas.gameObject, name);
        }

        private T InstantiateInto<T>(T prefab, GameObject gameObject, string name = "")
            where T : Component
        {
            var newGameObject = (T)Instantiate(prefab, gameObject.transform);
            if (!string.IsNullOrEmpty(name))
            {
                newGameObject.name = name;
            }
            else
            {
                newGameObject.name = prefab.name;
            }

            return newGameObject;
        }
    }
}