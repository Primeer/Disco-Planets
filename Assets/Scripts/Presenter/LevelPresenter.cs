using System;
using System.Threading;
using Configs.Windows;
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
        private readonly LevelEffectsService levelEffectsService;
        private readonly LevelView view;
        private readonly ScreenDispatcher screenDispatcher;
        private readonly BallView mainBall;

        private CancellationTokenSource effectsCts;
        

        public LevelPresenter(LevelView view, LevelModel levelModel, LevelEffectsService levelEffectsService, 
             SceneContext sceneContext, ScreenDispatcher screenDispatcher)
        {
            this.view = view;
            this.levelModel = levelModel;
            this.levelEffectsService = levelEffectsService;
            this.screenDispatcher = screenDispatcher;
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
            effectsCts?.Cancel();
            effectsCts = new CancellationTokenSource();
            PlayEffectsAsync(effectsCts.Token).Forget();
        }

        private async UniTaskVoid PlayEffectsAsync(CancellationToken token)
        {
            try
            {
                mainBall.gameObject.SetActive(false);
                await levelEffectsService.PlayLevelEndEffectsAsync(token);
                screenDispatcher.ShowWindow(WindowType.Win, OnNextLevelWindowClose);
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnNextLevelWindowClose()
        {
            levelModel.NextLevel();
        }
    }
}