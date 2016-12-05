﻿using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        private int currentStageIndex;
        private IGameStage currentStage;

        public void Start()
        {
            grid.InitializeBoard(SohgFactory);
            FaithPower = 0;
            TotalFaith = 0;
            currentStageIndex = -1;
            NextStage();
        }

        public void FixedUpdate()
        {
            if (currentStage != null)
            {
                currentStage.FixedUpdate();
            }
        }

        public void Update()
        {
            if (!IsPaused() && PlayerSociety != null)
            {
                GameInfoPanel.GameStatusInfo.SetValues(this);
            }
        }

        private void OnGridCellClick(ICell cell)
        {
            if (currentStage != null)
            {
                currentStage.OnCellClick(cell);
            }
        }
    }
}