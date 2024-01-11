using System;
using UnityEngine;

namespace Configs
{
    /// <summary>
    /// Данные уровня шара
    /// </summary>
    [Serializable]
    public struct BallValueData
    {
        [Tooltip("Иконка шара")]
        public Sprite sprite;
        
        [Tooltip("Модификатор размера шара")]
        public float scale;
        
        [Tooltip("Модификатор массы шара")]
        public float weigh;
    }
}