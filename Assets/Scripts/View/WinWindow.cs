using System;
using Service;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace View
{
    /// <summary>
    /// Вьюха окна окончания уровня
    /// </summary>
    public class WinWindow : MonoBehaviour
    {
        private const string Name = "WinWindow";
        private const string TitleLabelName = "text_title";
        private const string ContinueButtonName = "button_continue";
        private const string ScoreLabelName = "text_score";
        private const string BestScoreLabelName = "text_best_score";
        private const string MessageTextName = "text_message";
        
        private const int TitleTextId = 0;
        private const int ScoreTextId = 1;
        private const int BestScoreTextId = 2;
        private const int MessageTextId = 4;
        private const int ContinueTextId = 3;

        [Inject] private readonly LocalizationService localizationService;
        
        [SerializeField] private UIDocument document;

        private VisualElement root;
        private Label titleLabel;
        private Button continueButton;
        private Label scoreLabel;
        private Label bestScoreLabel;
        private Label messageLabel;

        private Action onResume;

        public bool Visible
        {
            get => root.style.display == DisplayStyle.Flex;
            set => root.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void ShowWindow(int score, int bestScore, bool withMessage = false, Action resumeAction = default)
        {
            onResume = resumeAction;
            Visible = true;

            SetScores(score, bestScore);

            if (withMessage)
            {
                messageLabel.style.display = DisplayStyle.Flex;
            }
            else
            {
                messageLabel.style.display = DisplayStyle.None;
            }
        }

        private void OnEnable()
        {
            root = document.rootVisualElement.Q(Name);
            titleLabel = root.Q<Label>(TitleLabelName);
            continueButton = root.Q<Button>(ContinueButtonName);
            scoreLabel = root.Q<Label>(ScoreLabelName);
            bestScoreLabel = root.Q<Label>(BestScoreLabelName);
            messageLabel = root.Q<Label>(MessageTextName);
            
            continueButton.RegisterCallback<ClickEvent>(OnContinue);

            Localize();
        }

        private void OnContinue(ClickEvent _)
        {
            onResume?.Invoke();
            Visible = false;
        }

        private void Localize()
        {
            var title = localizationService.GetLocalizedText(TitleTextId);
            var message = localizationService.GetLocalizedText(MessageTextId);
            var continueText = localizationService.GetLocalizedText(ContinueTextId);
            
            titleLabel.text = title;
            messageLabel.text = message;
            continueButton.text = continueText;
        }

        private void SetScores(int score, int bestScore)
        {
            var scoreText = localizationService.GetLocalizedTextWithParams(ScoreTextId, score);
            var bestScoreText = localizationService.GetLocalizedTextWithParams(BestScoreTextId, bestScore);
            
            scoreLabel.text = scoreText;
            bestScoreLabel.text = bestScoreText;
        }
    }
}