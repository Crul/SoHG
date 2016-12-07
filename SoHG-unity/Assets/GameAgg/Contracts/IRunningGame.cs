using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Collections;
using Sohg.TechnologyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        ISohgFactory SohgFactory { get; }

        ISpecies PlayerSpecies { get; }
        List<ISpecies> Species { get; }

        void ExecuteAction(IEnumerator actionExecution);
        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();
        void OnTechnologyActivated(ITechnology technology);
        IInstructions OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
    }
}
