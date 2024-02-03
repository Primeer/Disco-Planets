using System;
using Repository;
using Service;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class VibrationButtonPresenter : IInitializable, IDisposable
    {
        private readonly VibrationButtonView view;
        private readonly VibrationService vibrationService;
        private readonly SettingsRepository settings;

        public VibrationButtonPresenter(VibrationButtonView view, SettingsRepository settings, 
            VibrationService vibrationService)
        {
            this.view = view;
            this.settings = settings;
            this.vibrationService = vibrationService;
        }

        public void Initialize()
        {
            view.VibrationButtonClicked += OnVibrationButtonClick;
        }

        public void Dispose()
        {
            view.VibrationButtonClicked -= OnVibrationButtonClick;
        }

        private void OnVibrationButtonClick(bool state)
        {
            vibrationService.PlayVibration();
            settings.IsVibrationEnabled = state;
        }
    }
}