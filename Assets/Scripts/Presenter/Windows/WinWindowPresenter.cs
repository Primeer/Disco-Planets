using Configs;
using Configs.Levels;
using Model;
using Service;
using UnityEngine;
using VContainer;
using View.Windows;

namespace Presenter.Windows
{
    public class WinWindowPresenter : WindowPresenter
    {
        [Inject] private readonly ScoreModel scoreModel;
        [Inject] private readonly LevelModel levelModel;
        [Inject] private readonly GameService gameService;
        [Inject] private readonly LevelConfig levelConfig;
        [Inject] private readonly BallValuesConfig valuesConfig;

        public WinWindowPresenter(WindowView view)
        {
            View = view;
        }
        
        protected override void OnShow()
        {
            var view = (WinWindowView)View;
            
            view.SetScores(scoreModel.Score, scoreModel.BestScore);
            view.ShowMessage(levelModel.IsLastLevel());

            var hasNewBalls = HasNewBalls();
            view.ShowNewBalls(hasNewBalls, hasNewBalls ? GetBallsIcons() : null);
            
            view.ShowRestartButton(levelModel.IsLastLevel());
            view.RestartButtonClicked += OnRestartButtonClicked;
        }

        protected override void OnHide()
        {
            var view = (WinWindowView)View;
            view.RestartButtonClicked -= OnRestartButtonClicked;
        }

        private bool HasNewBalls()
        {
            if (levelModel.IsLastLevel())
            {
                return false;
            }
            
            var currentLevelData = levelConfig.GetLevelData(levelModel.Level);
            var nextLevelData = levelConfig.GetLevelData(levelModel.Level + 1);

            return currentLevelData.MinValue != nextLevelData.MinValue ||
                   currentLevelData.MaxThrowerValue != nextLevelData.MaxThrowerValue;
        }

        private Sprite[] GetBallsIcons()
        {
            var minBallValue = levelConfig.GetLevelData(levelModel.Level + 1).MinValue;
            var maxBallValue = levelConfig.GetLevelData(levelModel.Level + 1).MaxThrowerValue;
            
            var sprites = new Sprite[maxBallValue - minBallValue];

            for (int i = minBallValue, j = 0; i < maxBallValue; i++, j++)
            {
                sprites[j] = valuesConfig.Values[i].sprite;
            }

            return sprites;
        }

        private void OnRestartButtonClicked()
        {
            Dispatcher.CloseWindow(this);
            gameService.RestartGame();
        }
    }
}