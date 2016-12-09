using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using System.Linq;
using Sohg.TechnologyAgg;

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

        public abstract void Execute(ISociety society);
        
        public virtual bool IsActionEnabled(ISociety society)
        {
            return true;
        }

        protected override void Activate()
        {
            game.Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.AddAction(this));
        }
    }
}
