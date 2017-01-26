using Sohg.GameAgg.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietySkillDiscovery
    {
        void Initialize(IRunningGame game, ISkill skill, ISociety society);
    }
}
