using UnityEngine;
using Sohg.CrossCutting.Factories.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "SohgFactory", menuName = "SoHG/Sohg Factory")]
    public class SohgFactoryScript : ScriptableBaseObject, ISohgFactory
    {
        public PrefabFactoryScript PrefabFactory;
        
        public IGrid CreateGrid(Canvas canvas)
        {
            return PrefabFactory.InstantiateGrid(canvas);
        }
    }
}
