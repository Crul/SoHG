using Sohg.CrossCutting.Pooling;
using System.Linq;
using UnityEngine;

namespace Sohg.CrossCutting
{
    public abstract class BaseComponent : MonoBehaviour
    {
        protected void ReturnAllChildrenToPool(GameObject parent)
        {
            parent.GetComponentsInChildren<PooledObject>().ToList()
                .ForEach(child => child.ReturnToPool());
        }
    }
}
