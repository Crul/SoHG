using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        Canvas BoardOverCanvas { get; }
        IGameDefinition GameDefinition { get; }
        ISohgFactory SohgFactory { get; }
        IGrid Grid { get; }

        ISpecies PlayerSpecies { get; }
        List<ISociety> Societies { get; }
        List<ISpecies> Species { get; }
        int Year { get; set; }

        Coroutine ExecuteRoutine(IEnumerator actionExecution);
        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();
        void ResetGame();

        IInstructions OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
        void CloseSocietyInfo();
    }
}
