using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Collections;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        IGameDefinition GameDefinition { get; }
        List<ISociety> Societies { get; }
        ISohgFactory SohgFactory { get; }

        ISociety PlayerSociety { get; }
        int FaithPower { get; }
        int TotalFaith { get; }

        void ExecuteAction(IEnumerator actionExecution);
        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();
        void OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
    }
}
