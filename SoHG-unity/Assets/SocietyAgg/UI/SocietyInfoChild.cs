using Sohg.CrossCutting;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg.UI
{
    public abstract class SocietyInfoChild : BaseComponent, ISocietyInfoChild
    {
        public ISocietyAction SocietyAction { get; private set; }
        protected ISocietyInfo societyInfo { get; private set; }

        public virtual void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            SocietyAction = societyAction;
            this.societyInfo = societyInfo;
        }

        public abstract void SetEnable(bool isEnabled);
    }
}
