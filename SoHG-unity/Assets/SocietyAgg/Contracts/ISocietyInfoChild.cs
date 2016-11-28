namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyInfoChild
    {
        ISocietyAction SocietyAction { get; }

        void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo);
        void SetEnable(bool isEnabled);
    }
}
