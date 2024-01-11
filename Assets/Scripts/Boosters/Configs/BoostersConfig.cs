using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boosters.Configs
{
    /// <summary>
    /// Конфиг бустеров
    /// </summary>
    [CreateAssetMenu(fileName = "Boosters Config", menuName = "Scriptables/Boosters/Config", order = 0)]
    public class BoostersConfig : ScriptableObject
    {
        [Tooltip("Время отката бустера")]
        [SerializeField] private float cooldown;
        
        [Tooltip("Список бустеров")]
        [SerializeField] private BoosterData[] boosters;

        public float Cooldown => cooldown;
        public Dictionary<BoosterType, BoosterData> Boosters => boosters.ToDictionary(b => b.type, b => b);
    }
}