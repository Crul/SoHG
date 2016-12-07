using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;

namespace Sohg.TechnologyAgg
{
    public abstract class Technology : ScriptableBaseObject, ITechnology
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int faithCost;
        [SerializeField]
        private Sprite technologyIcon;
        
        public string Name { get { return name; } }
        public int FaithCost { get { return faithCost; } }
        public bool IsActive { get; private set; }
        public Sprite TechnologyIcon { get { return technologyIcon; } }

        public Technology()
        {
            Reset();
        }

        public bool Activate(IRunningGame game)
        {
            var hasFaithBeenConsumed = game.PlayerSpecies.ConsumeFaith(FaithCost);
            if (hasFaithBeenConsumed)
            {
                IsActive = true;
                game.ActivateActions();
                CustomActivation(game);
            }

            return IsActive;
        }

        public void Reset()
        { 
            IsActive = false;
        }

        protected virtual void CustomActivation(IRunningGame game) { }
    }
}
