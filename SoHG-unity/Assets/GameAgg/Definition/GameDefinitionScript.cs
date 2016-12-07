﻿using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Stages;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Actions;
using Sohg.TechnologyAgg;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.Definition
{
    [CreateAssetMenu(fileName = "GameDefinition", menuName = "SoHG/Game Definition")]
    public class GameDefinitionScript : ScriptableBaseObject, IGameDefinition
    {
        [SerializeField]
        private GameStageScript[] stages;
        [SerializeField]
        private TechnologyCategory[] technologyCategories;

        [Header("Society")]
        [SerializeField]
        private SocietyDefinitionScript playerSociety;
        [SerializeField]
        private SocietyDefinitionScript nonPlayerSociety;
        [Space]
        [SerializeField]
        private SocietyAction[] societyActions;

        public IGameStage[] Stages { get { return stages; } }
        public ITechnologyCategory[] TechnologyCategories { get { return technologyCategories; } }
        public ISocietyDefinition PlayerSociety { get { return playerSociety; } }
        public ISocietyDefinition NonPlayerSociety { get { return nonPlayerSociety; } }
        public ISocietyAction[] SocietyActions { get { return societyActions; } }
    }
}
