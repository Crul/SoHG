using Sohg.CrossCutting.Pooling;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    public abstract class SocietyInfoChild : PooledObject, ISocietyInfoChild
    {
        private ISocietyInfo societyInfo;
        protected ISocietyAction societyAction { get; private set; }

        protected IRunningGame game { get { return societyInfo.Game; } }
        protected ISociety society { get { return societyInfo.Society; } }

        public virtual void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            this.societyInfo = societyInfo;
            this.societyAction = societyAction;
        }
    }
}
