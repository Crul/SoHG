using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Collections;
using Sohg.GameAgg.UI;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        IGameDefinition GameDefinition { get; }
        ISohgFactory SohgFactory { get; }

        ISociety PlayerSociety { get; }
        List<ISociety> Societies { get; }
        int FaithPower { get; }
        int TotalFaith { get; }

        void ExecuteAction(IEnumerator actionExecution);
        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();
        void OnTechnologyActivated(ITechnology technology);
        IInstructions OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);

        void AddFaith(int faithAmount);
        bool ConsumeFaith(int faithAmount);
    }
}
