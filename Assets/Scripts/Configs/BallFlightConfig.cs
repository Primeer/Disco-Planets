using UnityEngine;

namespace Configs
{
    /// <summary>
    /// Конфиг значений, влияющих на физический полет шара
    /// </summary>
    [CreateAssetMenu(fileName = "Ball Config", menuName = "Scriptables/Ball Config", order = 0)]
    public class BallFlightConfig : ScriptableObject
    {
        [Tooltip("Начальная скорость вылета шара")]
        [SerializeField] private float startSpeed = 10f;
        
        [Tooltip("Сила приятжения к центральному шару")]
        [SerializeField] private float gravitationForce = 1f;
        
        [Tooltip("Дистанция полета выпущенного шара, на которой на него не действует гравитация и трение")]
        [SerializeField] private float freeFallDistance = 5f;
        
        [Tooltip("Максимальное трение")]
        [SerializeField] private float maxDrag = 1f;
        
        public float StartSpeed => startSpeed;
        public float GravitationForce => gravitationForce;
        public float FreeFallDistance => freeFallDistance;
        public float MaxDrag => maxDrag;
    }
}