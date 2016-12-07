﻿using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using System.Linq;
using Sohg.TechnologyAgg;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.SocietyAgg.Actions
{
    public abstract class SocietyAction : ScriptableBaseObject, ISocietyAction
    {
        [SerializeField]
        private Sprite actionIcon;
        [SerializeField]
        private int faithCost;
        [SerializeField]
        private Technology[] requiredTechnologies;
        
        protected IRunningGame game { get; private set; }

        public Sprite ActionIcon { get { return actionIcon; } }
        public int FaithCost { get { return faithCost; } }
        public bool IsActive { get; private set; }
        public ITechnology[] RequiredTechnologies { get { return requiredTechnologies; } }
        
        public void Initialize(IRunningGame game)
        {
            this.game = game;
        }

        public abstract void Execute(ISociety society);

        public bool Requires(ITechnology technology)
        {
            return RequiredTechnologies.Contains(technology);
        }
    }
}
