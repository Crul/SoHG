using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Collections;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        IGameDefinition Definition { get; }
        List<ISociety> Societies { get; }
        ISohgFactory SohgFactory { get; }

        void ExecuteAction(IEnumerator actionExecution);
        bool IsPaused();
        void NextStage();
        void OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
    }
}
