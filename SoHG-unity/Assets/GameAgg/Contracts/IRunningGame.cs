using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        ISohgFactory SohgFactory { get; }

        ISpecies PlayerSpecies { get; }
        List<ISpecies> Species { get; }

        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();

        IInstructions OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
    }
}
