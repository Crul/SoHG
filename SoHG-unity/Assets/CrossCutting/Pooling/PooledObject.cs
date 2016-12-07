using System;
using UnityEngine;

namespace Sohg.CrossCutting.Pooling
{
    // http://catlikecoding.com/unity/tutorials/object-pools/
    [DisallowMultipleComponent]
    public class PooledObject : BaseComponent
    {
        [NonSerialized]
        private ObjectPool poolInstanceForPrefab;

        public ObjectPool Pool { get; set; }

        public T GetPooledInstance<T>(GameObject gameObject) where T : PooledObject
        {
            if (!poolInstanceForPrefab)
            {
                poolInstanceForPrefab = ObjectPool.GetPool(this);
            }

            return (T)poolInstanceForPrefab.GetObject(gameObject);
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
