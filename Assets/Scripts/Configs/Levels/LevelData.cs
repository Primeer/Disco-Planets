using System;
using Service;
using Service.Features;
using UnityEngine;

namespace Configs.Levels
{
    /// <summary>
    /// Данные уровня
    /// </summary>
    [Serializable]
    public class LevelData
    {
        [Tooltip("Минимальное значение шара на уровне")]
        [SerializeField] private int minValue;
        
        [Tooltip("Максимальное значение шара на уровне = значение центрального шара")]
        [SerializeField] private int maxValue;
        
        [Tooltip("Максимальное значение шара, которым может выстрелить игрок")]
        [SerializeField] private int maxThrowerValue;
        
        [Tooltip("Количество очков, которое дается за мерж центрального шара")]
        [SerializeField] private int score;
        
        [Tooltip("Фичи, которые открываются на этом уровне")]
        [SerializeField] private FeatureEnum[] features;
        
        public int MinValue => minValue;
        public int MaxValue => maxValue;
        public int MaxThrowerValue => maxThrowerValue;
        public int Score => score;
        public FeatureEnum[] Features => features;
    }
}