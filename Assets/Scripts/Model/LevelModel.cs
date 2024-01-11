using Configs.Levels;

namespace Model
{
    /// <summary>
    /// Модель, хранящая текущий уровень. Предоставляет доступ к данным текущего уровня
    /// </summary>
    public class LevelModel
    {
        private readonly LevelConfig config;
        private int level;

        
        public LevelModel(LevelConfig config)
        {
            this.config = config;
        }
        
        public void SetLevel(int lvl)
        {
            level = lvl;
            EventBus.LevelChanged?.Invoke(level);
        }

        public void NextLevel()
        {
            SetLevel(level + 1);
        }

        public int GetMainBallScore()
        {
            return config.GetLevelData(level).Score;
        }

        public bool IsLastLevel()
        {
            return level + 1 == config.LevelsCount;
        }

        public bool IsNextLevelLast()
        {
            return level + 2 == config.LevelsCount;
        }
    }
}