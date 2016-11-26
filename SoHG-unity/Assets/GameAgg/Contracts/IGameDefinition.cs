using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameStage[] Stages { get; }

        ISocietyDefinition PlayerSociety { get; }
        ISocietyDefinition NonPlayerSociety { get; }
    }
}
