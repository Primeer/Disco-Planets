using System;

namespace Model
{
    /// <summary>
    /// Модель, хранящая текущий счет
    /// </summary>
    public class ScoreModel
    {
        public int Score { get; private set; }
        public int BestScore { get; private set; }
        
        public Action<int> ScoreChanged;
        public Action<int> BestScoreChanged;

        
        public void AddScore(int value)
        {
            Score += value;

            if (Score > BestScore)
            {
                BestScore = Score;
                BestScoreChanged?.Invoke(BestScore);
            }
            
            ScoreChanged?.Invoke(value);
        }

        public void SetBestScore(int score)
        {
            BestScore = score;
            BestScoreChanged?.Invoke(BestScore);
        }
    }
}