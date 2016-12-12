using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using System.Linq;
using Sohg.TechnologyAgg;
using System.Collections;

namespace Sohg.SocietyAgg.Actions
{
    public abstract class SocietyAction : TechnologyDependent, ISocietyAction
    {
        [SerializeField]
        private Sprite actionIcon;
        [SerializeField]
        private int faithCost;

        public Sprite ActionIcon { get { return actionIcon; } }
        public int FaithCost { get { return faithCost; } }

        public void Execute(ISociety society)
        {
            game.ExecuteRoutine(ExecuteRoutine(society));
        }
        
        public virtual bool IsActionEnabled(ISociety society)
        {
            return true;
        }

        protected override void Activate()
        {
            game.Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.AddAction(this));
        }

        protected abstract IEnumerator ExecuteAction(ISociety society);

        private IEnumerator ExecuteRoutine(ISociety society)
        {
            var effectAlreadyEnabled = (society.IsEffectActive[this]);
            if (!effectAlreadyEnabled)
            {
                society.IsEffectActive[this] = true;

                yield return game.ExecuteRoutine(ExecuteAction(society));

                society.IsEffectActive[this] = false;
            }
        }
    }
}
