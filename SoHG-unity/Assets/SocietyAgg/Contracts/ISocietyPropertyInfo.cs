﻿using Sohg.SocietyAgg.UI;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyPropertyInfo
    {
        void Initialize(SocietyProperty societyProperty);
        void SetSociety(ISociety society);
    }
}
