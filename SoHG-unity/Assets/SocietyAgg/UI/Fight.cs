﻿using Sohg.CrossCutting.Pooling;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    public class Fight : PooledObject, IFight
    {
        private IRunningGame game;
        private int duration;
        private int time;
        private IRelationship relationship;
        private ICell from;
        private ICell target;
        private System.Action resolveAttack;

        public void Awake()
        {
            time = 0;
        }

        public void FixedUpdate()
        {
            if (game == null || game.IsPaused())
            {
                return;
            }

            if (time < duration)
            {
                time++;
                CheckResult();
            }
        }

        public void Update()
        {
            if (game == null || game.IsPaused())
            {
                return;
            }

            transform.position = GetPosition();
            CheckResult();
        }

        public void Initialize(
            IRelationship relationship,
            ICell from,
            ICell target,
            IRunningGame game,
            System.Action resolveAttack)
        {
            this.game = game;
            this.relationship = relationship;
            this.from = from;
            this.target = target;
            duration = game.GameDefinition.FightDuration;
            this.resolveAttack = resolveAttack;
            time = 0;

            transform.position = GetPosition();
        }

        private Vector3 GetPosition()
        {
            var progress = ((float)time / duration);

            return new Vector3
            (
                ((1 - progress) * from.WorldPosition.x) + (progress * target.WorldPosition.x),
                ((1 - progress) * from.WorldPosition.y) + (progress * target.WorldPosition.y),
                -10
            );
        }

        private void CheckResult()
        {
            if (relationship.We.IsDead || relationship.Them.IsDead)
            {
                FinishFight();
            }
            else if (time >= duration && resolveAttack != null)
            {
                resolveAttack();
                FinishFight();
            }
        }

        private void FinishFight()
        {
            resolveAttack = null;
            ReturnToPool();
        }
    }
}
