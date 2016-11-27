using System;
using UnityEngine;

namespace Sohg.CrossCutting.Pooling
{
    // http://catlikecoding.com/unity/tutorials/object-pools/
    public class PooledObject : BaseComponent
    {
        [NonSerialized]
        private ObjectPool poolInstanceForPrefab;

        public ObjectPool Pool { get; set; }

        public T GetPooledInstance<T>(Canvas canvas) where T : PooledObject
        {
            if (!poolInstanceForPrefab)
            {
                poolInstanceForPrefab = ObjectPool.GetPool(this);
            }

            return (T)poolInstanceForPrefab.GetObject(canvas);
        }

        public void ReturnToPool()
        {
            if (Pool)
            {
                Pool.AddObject(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
