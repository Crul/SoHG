using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.GameAgg.Contracts
{
    public interface IRunningGame
    {
        List<ISociety> Societies { get; }
        ISohgFactory SohgFactory { get; }

        bool IsPaused();
        void NextStage();
    }
}
