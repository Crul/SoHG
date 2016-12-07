using Sohg.TechnologyAgg.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameStage[] Stages { get; }
        ISocietyAction[] SocietyActions { get; }
        ITechnologyCategory[] TechnologyCategories { get; }

        ISocietyDefinition PlayerSociety { get; }
        ISocietyDefinition NonPlayerSociety { get; }
    }
}
