using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IEvolvableGame : IRunningGame
    {
        IEndGame EndGame { get; }

        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        bool Invade(ICell from, ICell target);
        void Kill(ISociety society);
        void OnTechnologyActivated();
        void OnSkillActivated(ISkill skill, ISociety society);
        void Shrink(ISociety society);
        void Split(ISociety society);
    }
}
