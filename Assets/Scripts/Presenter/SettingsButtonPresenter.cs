using System;
using Configs.Windows;
using Service;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class SettingsButtonPresenter : IInitializable, IDisposable
    {
        private readonly SettingsButtonView view;
        private readonly ScreenDispatcher screenDispatcher;
        private readonly VibrationService vibrationService;

        public SettingsButtonPresenter(SettingsButtonView view, ScreenDispatcher screenDispatcher, 
            VibrationService vibrationService)
        {
            this.view = view;
            this.screenDispatcher = screenDispatcher;
            this.vibrationService = vibrationService;
        }

        public void Initialize()
        {
            view.ButtonClicked += OnButtonClicked;
        }

        public void Dispose()
        {
            view.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            vibrationService.PlayVibration();
            screenDispatcher.ShowWindow(WindowType.Settings);
        }
    }
}