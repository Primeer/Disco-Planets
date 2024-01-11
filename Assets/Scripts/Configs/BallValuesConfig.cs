using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    /// <summary>
    /// Конфиг с данными уровня шара
    /// </summary>
    [CreateAssetMenu(fileName = "Ball Values", menuName = "Scriptables/Ball Values", order = 0)]
    public class BallValuesConfig : ScriptableObject
    {
        public List<BallValueData> Values;
    }
}