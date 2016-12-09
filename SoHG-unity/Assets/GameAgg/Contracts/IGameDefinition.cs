using Sohg.TechnologyAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameStage[] Stages { get; }
        ITechnologyCategory[] TechnologyCategories { get; }

        ISpecies PlayerSpecies { get; }
        ISpecies[] NonPlayerSpecies { get; }
        ISkill[] Skills { get; }
        ISocietyAction[] SocietyActions { get; }
    }
}
