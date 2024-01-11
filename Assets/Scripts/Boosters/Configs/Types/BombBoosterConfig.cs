using UnityEngine;

namespace Boosters.Configs.Types
{
    /// <summary>
    /// Конфиг бустера "Бомба"
    /// </summary>
    [CreateAssetMenu(fileName = "Bomb Booster", menuName = "Scriptables/Boosters/Bomb", order = 0)]
    public class BombBoosterConfig : ScriptableObject
    {
        [Tooltip("Радиус окружности вокруг бомбы, в котором будут увеличены шары при взрыве")]
        [SerializeField] private float radius;
        
        [Tooltip("Сила, с которой будут откинуты шары вокруг бомбы при взрыве")]
        [SerializeField] private float force;
        
        [Tooltip("Значение, на которое будут увеличины шары вокруг бомбы при взрыве")]
        [SerializeField] private int value;
        
        [Tooltip("Префаб бомбы")]
        [SerializeField] private GameObject bombPrefab;
        
        [Tooltip("Префаб эффекта, который воспроизводится при взрыве")]
        [SerializeField] private GameObject bombEffectPrefab;
        
        public float Radius => radius;
        public float Force => force;
        public int Value => value;
        public GameObject BombPrefab => bombPrefab;
        public GameObject BombEffectPrefab => bombEffectPrefab;
    }
}