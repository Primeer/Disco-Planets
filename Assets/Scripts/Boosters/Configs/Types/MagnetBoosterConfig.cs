using UnityEngine;

namespace Boosters.Configs.Types
{
    /// <summary>
    /// Конфиг бустера "Магнит"
    /// </summary>
    [CreateAssetMenu(fileName = "Magnet Booster", menuName = "Scriptables/Boosters/Magnet", order = 0)]
    public class MagnetBoosterConfig : ScriptableObject
    {
        [Tooltip("Сила, с которой шары отталкиваются от центрального шара")]
        [SerializeField] private float force;
        
        [Tooltip("Задержка перед мержем, сек")]
        [SerializeField] private float delay;

        public float Force => force;
        public float Delay => delay;
    }
}