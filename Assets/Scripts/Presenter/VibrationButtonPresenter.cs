using System;
using Repository;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class VibrationButtonPresenter : IInitializable, IDisposable
    {
        private readonly VibrationButtonView view;
        private readonly SettingsRepository settings;

        public VibrationButtonPresenter(VibrationButtonView view, SettingsRepository settings)
        {
            this.view = view;
            this.settings = settings;
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
            settings.IsVibrationEnabled = state;
        }
    }
}