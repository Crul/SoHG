using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Actions
{
    public abstract class SocietyAction : ScriptableBaseObject, ISocietyAction
    {
        [SerializeField]
        private Sprite actionIcon;

        protected IRunningGame game { get; private set; }

        public Sprite ActionIcon { get { return actionIcon; } }

        public void Initialize(IRunningGame game)
        {
            this.game = game;
        }

        public abstract void Execute(ISociety society);
    }
}
