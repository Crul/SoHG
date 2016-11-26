using Sohg.CrossCutting.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting
{
    [CreateAssetMenu(fileName = "SohgConfig", menuName = "SoHG/Config")]
    public class SohgConfigScript : ScriptableBaseObject, ISohgConfig
    {
        [SerializeField]
        private int nonPlayerSocietyCount;

        public int NonPlayerSocietyCount { get { return nonPlayerSocietyCount; } }
    }
}
