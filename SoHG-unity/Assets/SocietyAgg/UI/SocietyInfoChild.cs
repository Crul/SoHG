using Sohg.CrossCutting.Pooling;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    public abstract class SocietyInfoChild : PooledObject
    {  
        protected ISocietyInfo societyInfo;
        protected IRunningGame game { get { return societyInfo.Game; } }
        protected ISociety society { get { return societyInfo.Society; } }
        
        protected void Initialize(ISocietyInfo societyInfo)
        {
            this.societyInfo = societyInfo;
        }
    }
}
