using UnityEngine;

namespace Configs.Levels
{
    /// <summary>
    /// Конфиг эффектов, которые воспроизводятся, когда игрок мержит центральный шар
    /// </summary>
    [CreateAssetMenu(fileName = "Change Level Effects Config", menuName = "Scriptables/Level Effects", order = 0)]
    public class ChangeLevelEffectsConfig : ScriptableObject
    {
        [Tooltip("Сила, с которой разлетаются шары при мерже центрального шара")]
        [SerializeField] private float explosionsForce;
        
        [Tooltip("Задержка появления окна окончания уровня при мерже центрального шара")]
        [SerializeField] private float nextLevelWindowDelay;
        
        public float ExplosionsForce => explosionsForce;
        public float NextLevelWindowDelay => nextLevelWindowDelay;
    }
}