﻿using Sohg.CrossCutting.Contracts;
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

        void ActivateActions();
        void ExecuteAction(IEnumerator actionExecution);
        List<ITechnology> GetActiveTechnologies();
        bool IsPaused();
        void Log(string log, params object[] logParams);
        void NextStage();

        IInstructions OpenInstructions(string instructionsText);
        void OpenSocietyInfo(ISociety society);
    }
}
