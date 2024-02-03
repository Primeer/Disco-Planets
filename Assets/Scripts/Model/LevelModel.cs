using Configs.Levels;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// Модель, хранящая текущий уровень. Предоставляет доступ к данным текущего уровня
    /// </summary>
    public class LevelModel
    {
        private readonly LevelConfig config;
        public int Level { get; set; }


        public LevelModel(LevelConfig config)
        {
            this.config = config;
        }
        
        public void SetLevel(int lvl)
        {
            Level = lvl;
            Debug.Log($"Level changed: {Level}");
            EventBus.LevelChanged?.Invoke(Level);
        }

        public void NextLevel()
        {
            if (IsLastLevel())
            {
                SetLevel(Level);
            }
            else
            {
                SetLevel(Level + 1);
            }
        }

        public void ResetLevels()
        {
            SetLevel(0);
        }

        public int GetMainBallScore()
        {
            return config.GetLevelData(Level).Score;
        }

        public bool IsLastLevel()
        {
            return Level + 1 == config.LevelsCount;
        }
    }
}