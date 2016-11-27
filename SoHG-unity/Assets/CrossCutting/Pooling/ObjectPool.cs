using System.Collections.Generic;
using UnityEngine;

namespace Sohg.CrossCutting.Pooling
{
    // http://catlikecoding.com/unity/tutorials/object-pools/
    public class ObjectPool : MonoBehaviour
    {
        private PooledObject prefab;
        private List<PooledObject> availableObjects = new List<PooledObject>();

        public static ObjectPool GetPool(PooledObject prefab)
        {
            GameObject obj;
            ObjectPool pool;
            if (Application.isEditor)
            {
                obj = GameObject.Find(prefab.name + " Pool");
                if (obj)
                {
                    pool = obj.GetComponent<ObjectPool>();
                    if (pool)
                    {
                        return pool;
                    }
                }
            }
            obj = new GameObject(prefab.name + " Pool");
            DontDestroyOnLoad(obj);
            pool = obj.AddComponent<ObjectPool>();
            pool.prefab = prefab;

            return pool;
        }

        public PooledObject GetObject(Canvas canvas)
        {
            PooledObject obj;
            int lastAvailableIndex = availableObjects.Count - 1;
            if (lastAvailableIndex >= 0)
            {
                obj = availableObjects[lastAvailableIndex];
                availableObjects.RemoveAt(lastAvailableIndex);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Instantiate(prefab);
                obj.transform.SetParent(canvas.transform, false);
                obj.Pool = this;
            }

            return obj;
        }

        public void AddObject(PooledObject obj)
        {
            obj.gameObject.SetActive(false);
            availableObjects.Add(obj);
        }
    }
}