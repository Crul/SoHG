﻿using Sohg.GameAgg.Contracts;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyBox
    {
        void Initialize(IEvolvableGame game, ITechnology technology,
            ITechnologyStatesSetter technologyStatesSetter);
        void SetState(int faithPower);
    }
}
