using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model;
using Service;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class LevelPresenter : IInitializable, IStartable, IDisposable
    {
        private const string SaveKey = "level";
        
        private readonly LevelModel levelModel;
        private readonly ChangeLevelModel changeLevelModel;
        private readonly ScoreModel scoreModel;
        private readonly LevelView view;
        private readonly ScreenDispatcher screenDispatcher;
        private readonly BallView mainBall;

        private CancellationTokenSource effectsCts;
        

        public LevelPresenter(LevelView view, LevelModel levelModel, ChangeLevelModel changeLevelModel, 
            ScreenDispatcher screenDispatcher, SceneContext sceneContext, ScoreModel scoreModel)
        {
            this.view = view;
            this.levelModel = levelModel;
            this.changeLevelModel = changeLevelModel;
            this.screenDispatcher = screenDispatcher;
            this.scoreModel = scoreModel;
            mainBall = sceneContext.MainBall;
        }

        public void Initialize()
        {
            EventBus.LevelChanged += OnLevelChanged;
            EventBus.MainBallMerged += OnMainBallMerged;
        }

        public void Start()
        {
            int savedLevel = PlayerPrefs.GetInt(SaveKey, 0);
            levelModel.SetLevel(savedLevel);
        }

        public void Dispose()
        {
            EventBus.LevelChanged -= OnLevelChanged;
            EventBus.MainBallMerged -= OnMainBallMerged;
            
            effectsCts?.Cancel();
            effectsCts?.Dispose();
            effectsCts = null;
        }

        private void OnLevelChanged(int level)
        {
            view.SetLevelText(level + 1);
            PlayerPrefs.SetInt(SaveKey, level);
        }

        private void OnMainBallMerged()
        {
            if (levelModel.IsLastLevel())
            {
                return;
            }
            
            effectsCts?.Cancel();
            effectsCts = new CancellationTokenSource();
            PlayEffectsAsync(effectsCts.Token).Forget();
        }

        private async UniTaskVoid PlayEffectsAsync(CancellationToken token)
        {
            mainBall.gameObject.SetActive(false);
            await changeLevelModel.PlayLevelEndEffectsAsync(token);
            screenDispatcher.ShowWinWindow(scoreModel.Score, scoreModel.BestScore, levelModel.IsNextLevelLast(), OnNextLevelWindowClose);
        }

        private void OnNextLevelWindowClose()
        {
            levelModel.NextLevel();
            mainBall.gameObject.SetActive(true);
        }
    }
}