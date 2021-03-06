﻿using Sohg.SocietyAgg.UI;
using Grids2D;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using Sohg.SocietyAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.GameAgg.Contracts;
using Sohg.CrossCutting.Pooling;
using Sohg.TechnologyAgg.Contracts;
using Sohg.TechnologyAgg.UI;
using System;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "PrefabFactory", menuName = "SoHG/Prefab Factory")]
    public class PrefabFactory : ScriptableBaseObject
    {
        // TODO instantiate prefabs into "folder objects" to avoid canvas-children-hell        
        [SerializeField]
        private Boat boatPrefab;
        [SerializeField]
        private FaithRecolectable faithRecolectablePrefab;
        [SerializeField]
        private Fight fightPrefab;

        [SerializeField]
        private SocietyMarker societyMarkerPrefab;
        [SerializeField]
        private SocietyActionButton societyActionButtonPrefab;
        [SerializeField]
        private SocietyEffectIcon societyEffectIconPrefab;
        [SerializeField]
        private SocietyPropertyInfo societyPropertyInfoPrefab;
        [SerializeField]
        private SocietySkillDiscovery societySkillDiscoveryPrefab;
        [SerializeField]
        private SocietySkillIcon societySkillIconPrefab;

        [SerializeField]
        private TechnologyBox technologyBoxPrefab;
        [SerializeField]
        private TechnologyCategoryColumn technologyCategoryColumnPrefab;

        public IBoat InstantiateBoat(Canvas canvas, string name)
        {
            return InstantiatePooledInto(boatPrefab, canvas.gameObject, name);
        }

        public IFaithRecolectable InstantiateFaithRecolectable(Canvas canvas, string name)
        {
            return InstantiatePooledInto(faithRecolectablePrefab, canvas.gameObject, name);
        }

        public IFight InstantiateFight(Canvas canvas, string name)
        {
            return InstantiatePooledInto(fightPrefab, canvas.gameObject, name);
        }

        public ISocietyMarker InstantiateSocietyMarker(Canvas canvas, string name)
        {
            var societyMarker = InstantiateIntoCanvas(societyMarkerPrefab, canvas, name);
            var societyMarkerPosition = societyMarker.transform.position;
            societyMarkerPosition.z = -25; // TODO why z = -50 needed
            societyMarker.transform.position = societyMarkerPosition;

            return societyMarker;
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

        public ISocietySkillDiscovery InstantiateSocietySkillDiscovery(Canvas canvas, string name)
        {
            return InstantiatePooledInto(societySkillDiscoveryPrefab, canvas.gameObject, name);
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

        public ITechnologyCategoryColumn InstantiateTechnologyCategoryColumn(GameObject gameObject, string name)
        {
            var technologyCategoryColumn = InstantiateInto(technologyCategoryColumnPrefab, gameObject, name);
            technologyCategoryColumn.transform.localScale = Vector3.one; // TODO why scale = 1 needed?

            return technologyCategoryColumn;
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