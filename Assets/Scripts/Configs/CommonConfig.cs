using UnityEngine;

namespace Configs
{
    /// <summary>
    /// Конфиг со значениями, влияющими на общие аспекты игры
    /// </summary>
    [CreateAssetMenu(fileName = "Common Config", menuName = "Scriptables/Common Config", order = 0)]
    public class CommonConfig : ScriptableObject
    {
        [Tooltip("Время задержки спавна шара после вылета предыдущего")]
        [SerializeField] private float throwerBallSpawnDelaySec;
        
        [Tooltip("Максимальное количество шаров на поле до проигрыша")]
        [SerializeField] private int ballsLimit;
        
        [Tooltip("При превышении лимита шаров на поле запускается таймер, по истечении которого игрок проигрывает")]
        [SerializeField] private float gameOverTimer;
        
        [Space]
        [Tooltip("Длительность вибрации, мс")]
        [SerializeField] private int vibrationDurationMs;
        
        [Tooltip("Сила вибрации")]
        [Range(1, 255)]
        [SerializeField] private int vibrationAmplitude;
        
        public float ThrowerBallSpawnDelaySec => throwerBallSpawnDelaySec;
        public int BallsLimit => ballsLimit;
        public float GameOverTimer => gameOverTimer;
        public int VibrationDurationMs => vibrationDurationMs;
        public int VibrationAmplitude => vibrationAmplitude;
    }
}