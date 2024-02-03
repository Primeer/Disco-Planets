using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.Windows
{
    public class WinWindowView : WindowView
    {
        private const string ScoreLabelName = "text_score";
        private const string BestScoreLabelName = "text_best_score";
        private const string MessageLabelName = "text_message";
        private const string NewBallsPanelName = "panel_new_balls";
        private const string NewBallsContentPanelName = "panel_new_balls_content";
        private const string RestartButtonName = "button_restart";

        private Label scoreLabel;
        private Label bestScoreLabel;
        private Label messageLabel;
        private VisualElement newBallsPanel;
        private VisualElement newBallsContentPanel;
        private Button restartButton;
        private string scoreText;
        private string bestScoreText;

        public Action RestartButtonClicked;

        public override void Initialize(VisualElement root)
        {
            base.Initialize(root);
            
            restartButton = Root.Q<Button>(RestartButtonName);
            restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);
        }

        public void SetScores(int score, int bestScore)
        {
            scoreLabel ??= Root.Q<Label>(ScoreLabelName);
            scoreText ??= scoreLabel.text;
            scoreLabel.text = string.Format(scoreText, score); 
            
            bestScoreLabel ??= Root.Q<Label>(BestScoreLabelName);
            bestScoreText ??= bestScoreLabel.text;
            bestScoreLabel.text = string.Format(bestScoreText, bestScore);
        }

        public void ShowMessage(bool isShowed)
        {
            messageLabel ??= Root.Q<Label>(MessageLabelName);
            messageLabel.style.display = isShowed ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void ShowNewBalls(bool isActive, [CanBeNull]Sprite[] icons)
        {
            newBallsPanel ??= Root.Q<VisualElement>(NewBallsPanelName);
            newBallsPanel.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
            
            newBallsContentPanel ??= Root.Q<VisualElement>(NewBallsContentPanelName);
            newBallsContentPanel.Clear();

            if (!isActive || icons == null)
            {
                return;
            }
            
            foreach (var icon in icons)
            {
                var element = new VisualElement
                {
                    style =
                    {
                        backgroundImage = new StyleBackground(icon),
                        width = new StyleLength(new Length(150)),
                        height = new StyleLength(new Length(150))
                    }
                };

                newBallsContentPanel.Add(element);
            }
        }
        
        public void ShowRestartButton(bool isActive)
        {
            restartButton.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
        }
        
        private void OnRestartButtonClicked(ClickEvent _)
        {
            RestartButtonClicked?.Invoke();
        }
    }
}