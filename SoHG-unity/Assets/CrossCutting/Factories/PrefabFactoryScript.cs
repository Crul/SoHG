using Sohg.SocietyAgg.UI;
using Grids2D;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using Sohg.SocietyAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.GameAgg.Contracts;
using Sohg.CrossCutting.Pooling;
using System;
using Sohg.TechnologyAgg.Contracts;
using Sohg.TechnologyAgg.UI;

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
        private Instructions instructionsPrefab;

        [SerializeField]
        private FaithRecolectable faithRecolectablePrefab;
        [SerializeField]
        private Fight fightPrefab;

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
        private SocietySkillIcon societySkillIconPrefab;

        [SerializeField]
        private TechnologyBox technologyBoxPrefab;
        [SerializeField]
        private TechnologyCategoryBox technologyCategoryBoxPrefab;

        public IGrid InstantiateGrid(Canvas canvas)
        {
            return InstantiateIntoCanvas(gridPrefab, canvas, "Grid2D");
        }

        public IFaithRecolectable InstantiateFaithRecolectable(Canvas canvas, string name)
        {
            return InstantiatePooledInto(faithRecolectablePrefab, canvas.gameObject, name);
        }

        public IEndGame InstantiateEndGame(Canvas canvas)
        {
            return InstantiateIntoCanvas(endGamePrefab, canvas, "EndGame");
        }

        public IInstructions InstantiateInstructions(Canvas canvas)
        {
            var instructions = InstantiateIntoCanvas(instructionsPrefab, canvas, "Instructions");
            instructions.transform.localScale = Vector3.one; // TODO why scale = 1 needed?

            return instructions;
        }

        public ISocietyMarker InstantiateSocietyMarker(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(societyMarkerPrefab, canvas, name);
        }

        public IFight InstantiateFight(Canvas canvas, string name)
        {
            return InstantiatePooledInto(fightPrefab, canvas.gameObject, name);
        }

        public ISocietyInfo InstantiateSocietyInfo(Canvas canvas, string name)
        {
            return InstantiateIntoCanvas(societyInfoPrefab, canvas, name);
        }

        public ISocietyActionButton InstantiateSocietyActionButton(GameObject gameObject, string name)
        {
            return InstantiatePooledInto(societyActionButtonPrefab, gameObject, name);
        }

        public ISocietyEffectIcon InstantiateSocietyEffectIcon(GameObject gameObject, string name)
        {
            return InstantiatePooledInto(societyEffectIconPrefab, gameObject, name);
        }

        public ISocietyPropertyInfo InstantiateSocietyPropertyInfo(GameObject gameObject, string name)
        {
            return InstantiatePooledInto(societyPropertyInfoPrefab, gameObject, name);
        }

        public ISocietySkillIcon InstantiateSocietySkillIcon(GameObject gameObject, string name)
        {
            return InstantiatePooledInto(societySkillIconPrefab, gameObject, name);
        }

        public ITechnologyBox InstantiateTechnologyBox(GameObject gameObject, string name)
        {
            var technologyBox = InstantiateInto(technologyBoxPrefab, gameObject, name);
            technologyBox.transform.localScale = Vector3.one; // TODO why scale = 1 needed?

            return technologyBox;
        }

        public ITechnologyCategoryBox InstantiateTechnologyCategoryBox(GameObject gameObject, string name)
        {
            var technologyCategoryBox = InstantiateInto(technologyCategoryBoxPrefab, gameObject, name);
            technologyCategoryBox.transform.localScale = Vector3.one; // TODO why scale = 1 needed?

            return technologyCategoryBox;
        }

        public T InstantiatePooledInto<T>(T prefab, GameObject gameObject, string name = "")
            where T : PooledObject
        {
            var pooledObject = prefab.GetPooledInstance<T>(gameObject);
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