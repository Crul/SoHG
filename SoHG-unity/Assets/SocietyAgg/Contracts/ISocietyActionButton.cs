namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyActionButton : ISocietyInfoActionElement
    {
        void Initialize(ISocietyAction action, ISocietyInfo societyInfo);
    }
}
