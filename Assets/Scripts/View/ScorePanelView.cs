using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вью панели со счетом игрока
    /// </summary>
    public class ScorePanelView : MonoBehaviour
    {
        private const string ScoreLabelName = "text_score";
        private const string BestScoreLabelName = "text_best_score";
        
        [Tooltip("Задержка обновления числа на панели очков")]
        [SerializeField] private float scoreUpdateDelay;
        [SerializeField] private UIDocument document;

        private Label scoreLabel;
        private Label bestScoreLabel;

        private int currentScore;
        private int deltaScore;
        private float timer;
        
        public void AddDeltaScore(int delta)
        {
            deltaScore += delta;
        }
        
        public void SetBestScore(int score)
        {
            bestScoreLabel.text = score.ToString();
        }
        
        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            scoreLabel = root.Q<Label>(ScoreLabelName);
            bestScoreLabel = root.Q<Label>(BestScoreLabelName);
        }

        private void Update()
        {
            if (deltaScore == 0)
            {
                return;
            }

            timer += Time.deltaTime;

            if (timer < scoreUpdateDelay)
            {
                return;
            }
            
            currentScore++;
            deltaScore--;
            
            scoreLabel.text = currentScore.ToString();
            
            timer = 0;
        }
    }
}