using System.Collections.Generic;
using UnityEngine;

namespace Configs.Levels
{
    /// <summary>
    /// Конфиг с данными уровней
    /// </summary>
    [CreateAssetMenu(fileName = "Level Config", menuName = "Scriptables/Levels", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [Tooltip("Список уровней")]
        [SerializeField] private List<LevelData> levels;
        
        public int LevelsCount => levels.Count;

        
        public LevelData GetLevelData(int level)
        {
            if (level < 0 || level >= levels.Count)
            {
                throw new System.Exception($"Level {level.ToString()} is out of range");
            }
            
            return levels[level];
        }
    }
}