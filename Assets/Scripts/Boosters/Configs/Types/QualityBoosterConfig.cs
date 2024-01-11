using UnityEngine;

namespace Boosters.Configs.Types
{
    /// <summary>
    /// Конфиг бустера "Качество"
    /// </summary>
    [CreateAssetMenu(fileName = "Quality Booster", menuName = "Scriptables/Boosters/Quality", order = 0)]
    public class QualityBoosterConfig : ScriptableObject
    {
        [Tooltip("Число шаров, которые будут увеличены")]
        [SerializeField] private int count;
        
        [Tooltip("Значение, на которое будут увеличены шары")]
        [SerializeField] private int value;
        
        public int Count => count;
        public int Value => value;
    }
}