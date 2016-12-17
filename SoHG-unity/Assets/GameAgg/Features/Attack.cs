﻿using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;
using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using Sohg.CrossCutting.Contracts;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "AttackFeature", menuName = "SoHG/Features/Attack")]
    public class Attack : GameFeature
    {
        private enum AttackResult { Undefined, Win, Tie, Loose }

        private Dictionary<ISociety, List<int>> attackInvolvedCells;

        public Attack()
        {
            attackInvolvedCells = new Dictionary<ISociety, List<int>>(); // TODO reset on new game?
        }

        public override void Run(IEvolvableGame game, ISociety society)
        {
            if (!attackInvolvedCells.ContainsKey(society))
            {
                attackInvolvedCells.Add(society, new List<int>());
            }

            if (attackInvolvedCells[society].Count >= society.State.MaximumAttacks)
            {
                return;
            }

            society.Relationships
                .Where(relationship => relationship.AreWeNeighbours()
                                    && relationship.WillingToAttack(game.SohgFactory.Config))
                .SelectMany(relationship => relationship.MyFrontierCellIndices
                    .Select(cellIndex => new
                    {
                        from = cellIndex,
                        relationship = relationship
                    }))
                .OrderBy(attackInfo => Random.Range(0f, 1f))
                .ToList()
                .ForEach(attackInfo => ExecuteAttack(game, attackInfo.from, attackInfo.relationship));

        }

        private void ExecuteAttack(IEvolvableGame game, int fromCellIndex, IRelationship relationship)
        {
            var we = relationship.We;
            if (attackInvolvedCells[we].Count >= we.State.MaximumAttacks)
            {
                return;
            }

            var fightCreated = CreateFight(game, relationship, fromCellIndex);
            if (fightCreated)
            {
                attackInvolvedCells[we].Add(fromCellIndex);
            }
        }

        private bool CreateFight(IEvolvableGame game, IRelationship relationship, int fromCellIndex)
        {
            var from = game.Grid.GetCell(fromCellIndex);
            if (from.IsInvolvedInAttack)
            {
                return false;
            }

            var target = game.Grid.GetInvadableCell(from, relationship.Them.Territory);

            if (target == null || target.IsInvolvedInAttack)
            {
                return false;
            }

            from.IsInvolvedInAttack = true;
            target.IsInvolvedInAttack = true;

            game.SohgFactory
                .CreateFight(relationship, from, target, () => ResolveAttack(game, relationship, from, target));

            return true;
        }


        public void ResolveAttack(IEvolvableGame game, IRelationship relationship, ICell from, ICell target)
        {
            var we = relationship.We;
            var them = relationship.Them;
            
            var damageRate = (float)System.Math.Pow(we.State.Power / them.State.Power, 10);
            var result = GetResult(damageRate, game.SohgFactory.Config);
            
            var ourDeads = GetDeads(we, 1 / damageRate);
            var thriDeads = GetDeads(them, damageRate);
            
            we.State.Kill(ourDeads);
            them.State.Kill(thriDeads);

            switch (result)
            {
                case AttackResult.Win:
                    if (we.State.ExpansionCapacity > 0)
                    {
                        var theirDensityBeforeAttack = them.State.PopulationDensity;
                        if (game.Invade(from, target))
                        {
                            var invasionDeads = System.Convert.ToInt64(theirDensityBeforeAttack);
                            relationship.Them.State.Kill(invasionDeads);
                        }
                    }
                    // TODO update stats on win
                    break;
                case AttackResult.Loose:
                    // TODO update stats on loose
                    break;
                case AttackResult.Tie:
                    break;
            }
            // TODO update stats on fight

            attackInvolvedCells[we].Remove(from.CellIndex);
            from.IsInvolvedInAttack = false;
            target.IsInvolvedInAttack = false;
        }
        
        private long GetDeads(ISociety society, float damageRate)
        {
            return System.Convert.ToInt64
                (society.State.PopulationDensity * damageRate / (5 + damageRate));
        }

        private AttackResult GetResult(float damageRate, ISohgConfig config)
        {
            var result = damageRate * Random.Range(0.8f, 1.2f);

            if (System.Math.Abs(result - 1) < config.AttackDamageTieRateThreshold)
                return AttackResult.Tie;

            if (result > 1)
                return AttackResult.Win;

            return AttackResult.Loose;
        }

    }
}
