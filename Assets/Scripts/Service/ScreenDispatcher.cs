using System;
using System.Collections.Generic;
using Configs.Windows;
using Presenter.Windows;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Service
{
    /// <summary>
    /// Управляет отображением окон
    /// </summary>
    public class ScreenDispatcher : IStartable
    {
        private const string WindowsRootName = "windows";

        private readonly Dictionary<WindowType, WindowPresenter> windowsPresentersByTypes;
        private readonly UIDocument uiDocument;
        private readonly GameService gameService;
        private readonly VibrationService vibrationService;
        private readonly IObjectResolver container;
        private readonly Dictionary<WindowType, WindowPresenter> presenters = new();
        
        private VisualElement windowsRoot;
        private int openWindowsCount;
        
        public ScreenDispatcher(IObjectResolver container, UIDocument uiDocument, WindowsConfig windowsConfig, 
            VibrationService vibrationService, GameService gameService)
        {
            this.container = container;
            this.uiDocument = uiDocument;
            this.vibrationService = vibrationService;
            this.gameService = gameService;
            windowsPresentersByTypes = windowsConfig.Presenters;
        }

        public void Start()
        {
            windowsRoot = uiDocument.rootVisualElement.Q<VisualElement>(WindowsRootName);
        }

        public void ShowWindow(WindowType type, Action onResume = null)
        {
            if (!presenters.TryGetValue(type, out var presenter))
            {
                presenter = SpawnWindow(type);
            }

            presenter.OnResume = onResume;
            // presenter.ContinueButtonClicked = () => CloseWindow(presenter);

            OnWindowOpen(presenter);
        }

        private WindowPresenter SpawnWindow(WindowType type)
        {
            if (!windowsPresentersByTypes.TryGetValue(type, out var presenter))
            {
                presenter = new WindowPresenter();
            }
            
            container.Inject(presenter);
            presenter.Initialize(type, windowsRoot, this);
            presenters.Add(type, presenter);
            
            return presenter; 
        }
        
        private void OnWindowOpen(WindowPresenter presenter)
        {
            openWindowsCount++;
            CheckWindowsCount();
            
            presenter.WindowElement.BringToFront();
            presenter.Show();
        }

        public void CloseWindow(WindowPresenter presenter)
        {
            vibrationService.PlayVibration();
            presenter.Hide();
            
            openWindowsCount--;
            CheckWindowsCount();
        }

        private void CheckWindowsCount()
        {
            if (openWindowsCount == 0)
            {
                gameService.UnfreezeGame();
            }
            else
            {
                gameService.FreezeGame();
            }
        }
    }
}