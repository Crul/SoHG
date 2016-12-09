using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;
using Sohg.CrossCutting;

namespace Sohg.TechnologyAgg
{
    public abstract class TechnologyDependent : ScriptableBaseObject, ITechnologyDependent
    {
        [SerializeField]
        private Technology[] requiredTechnologies;

        protected bool isActive { get; private set; }
        protected IWarPlayable game { get; private set; }

        public ITechnology[] RequiredTechnologies { get { return requiredTechnologies; } }

        public void CheckActivation()
        {
            if (isActive)
            {
                return;
            }

            var allRequiredTechnologiesActivated = game.CheckDependentTechnologies(this);
            if (allRequiredTechnologiesActivated)
            {
                Activate();
                isActive = true;
            }
        }

        public void Initialize(IWarPlayable game)
        {
            isActive = false;
            this.game = game;
            CheckActivation();
        }

        protected abstract void Activate();
    }
}
