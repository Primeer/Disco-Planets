using System;
using Configs;
using Configs.Windows;
using Repository;
using Service;
using Service.Features;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class DebugPresenter : IInitializable, IStartable, IDisposable
    {
        private readonly DebugConfig config;
        private readonly DebugView view;
        private readonly DebugLogsRepository logsRepository;
        private readonly ScreenDispatcher screenDispatcher;
        private readonly FeatureOpenService featureOpenService;
        private readonly GameService gameService;
        private readonly GameObject fpsObject;
        

        public DebugPresenter(DebugView view, DebugConfig config, DebugLogsRepository logsRepository, 
            ScreenDispatcher screenDispatcher, FeatureOpenService featureOpenService, 
            SceneContext sceneContext, GameService gameService)
        {
            this.view = view;
            this.config = config;
            this.logsRepository = logsRepository;
            this.screenDispatcher = screenDispatcher;
            this.featureOpenService = featureOpenService;
            this.gameService = gameService;
            fpsObject = sceneContext.FpsObject;
        }

        public void Initialize()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            view.NextLevelButtonClicked += OnNextLevelButtonClicked;
            view.ClearPrefsButtonClicked += OnClearPrefsButtonClicked;
            view.LogsButtonClicked += OnLogsButtonClicked;
            view.FeaturesButtonClicked += OnFeatureOpenButtonClicked;
            Application.logMessageReceived += HandleLog;
#endif
        }

        public void Start()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            view.SpawnElements(config.gameElementsUxml);
            fpsObject.SetActive(true);
#endif
        }

        public void Dispose()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            view.NextLevelButtonClicked -= OnNextLevelButtonClicked;
            view.ClearPrefsButtonClicked -= OnClearPrefsButtonClicked;
            view.LogsButtonClicked -= OnLogsButtonClicked;
            view.FeaturesButtonClicked -= OnFeatureOpenButtonClicked;
            Application.logMessageReceived -= HandleLog;
#endif
        }

        private void OnNextLevelButtonClicked()
        {
            EventBus.MainBallMerged?.Invoke();
        }
        
        private void OnClearPrefsButtonClicked()
        {
            PlayerPrefs.DeleteAll();
            gameService.RestartGame();
        }

        private void OnLogsButtonClicked()
        {
            screenDispatcher.ShowWindow(WindowType.Logs);
        }

        private void OnFeatureOpenButtonClicked()
        {
            featureOpenService.OpenAllFeatures();
        }
        
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            logsRepository.Logs.Add(logString);
        }
    }
}