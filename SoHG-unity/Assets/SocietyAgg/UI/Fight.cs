using Sohg.CrossCutting.Pooling;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System;
using UnityEngine;

namespace Sohg.SocietyAgg.UI
{
    public class Fight : PooledObject, IFight
    {
        private int time;
        private ICell from;
        private ICell target;
        private Action resolveAttack;

        public void Awake()
        {
            time = 16;
        }

        public void FixedUpdate()
        {
            if (time > 0)
            {
                time--;
                CheckTime();
            }
        }

        public void Update()
        {
            if (time > 0)
            {
                CheckTime();
            }
        }

        public void Initialize(ICell from, ICell target, int time, Action resolveAttack)
        {
            this.from = from;
            this.target = target;
            this.time = time;
            this.resolveAttack = resolveAttack;
            
            transform.position = new Vector3
            (
                (from.WorldPosition.x + target.WorldPosition.x) / 2,
                (from.WorldPosition.y + target.WorldPosition.y) / 2,
                -10
            ); ;
        }

        private void CheckTime()
        {
            if (time == 0)
            {
                resolveAttack();
                ReturnToPool();
            }
        }
    }
}
