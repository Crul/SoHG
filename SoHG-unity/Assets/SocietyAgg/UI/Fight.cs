using Sohg.CrossCutting.Pooling;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.UI
{
    public class Fight : PooledObject, IFight
    {
        private int time;
        private IRelationship relationship;
        private ICell from;
        private ICell target;
        private System.Action resolveAttack;

        public void Awake()
        {
            time = 10 + Random.Range(0, 6);
        }

        public void FixedUpdate()
        {
            if (time > 0)
            {
                time--;
                CheckResult();
            }
        }

        public void Update()
        {
            CheckResult();
        }

        public void Initialize(IRelationship relationship, ICell from, ICell target, int time, System.Action resolveAttack)
        {
            this.relationship = relationship;
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

        private void CheckResult()
        {
            if (relationship.We.IsDead || relationship.Them.IsDead)
            {
                FinishFight();
            }
            else if (time == 0 && resolveAttack != null)
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
