using System;
using Configs.Windows;
using UnityEngine;

namespace Boosters.Configs
{
    [Serializable]
    public class BoosterData
    {
        [Tooltip("Тип бустера")]
        public BoosterType type;
        
        [Tooltip("Иконка бустера")]
        public Sprite sprite;
        
        [Tooltip("Окно туториала")]
        public WindowType tutorialWindow;
    }
}