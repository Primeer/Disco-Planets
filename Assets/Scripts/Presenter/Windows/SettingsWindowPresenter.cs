using System.Linq;
using Configs;
using Configs.Windows;
using Repository;
using Service;
using UnityEngine.UIElements;
using VContainer;

namespace Presenter.Windows
{
    public class SettingsWindowPresenter : WindowPresenter
    {
        private const string PanelContentName = "panel_content";
        private const string WindowTitleName = "text_title";
        private const string VibrationButtonName = "button_vibration";
        private const string VibrationOnPanelName = "panel_vibration_toggle_on";
        
        [Inject] private readonly SettingsConfig settingsConfig;
        [Inject] private readonly ScreenDispatcher screenDispatcher;
        [Inject] private readonly TutorialService tutorialService;
        [Inject] private readonly VibrationService vibrationService;
        [Inject] private readonly SettingsRepository settingsRepository;

        protected override void OnShow()
        {
            SpawnWindowsButtons();
            SetVibrationButton();
        }

        protected override void OnHide()
        {
            var contentPanel = WindowElement.Q<VisualElement>(PanelContentName);
            contentPanel.Clear();
        }

        private void SetVibrationButton()
        {
            var vibrationOnPanel = WindowElement.Q<VisualElement>(VibrationOnPanelName);
            vibrationOnPanel.style.visibility = settingsRepository.IsVibrationEnabled ? Visibility.Visible : Visibility.Hidden;
            
            var vibrationButton = WindowElement.Q<Button>(VibrationButtonName);
            vibrationButton.RegisterCallback<ClickEvent>(OnVibrationButtonClicked);
        }

        private void SpawnWindowsButtons()
        {
            var contentPanel = WindowElement.Q<VisualElement>(PanelContentName);
            var types = settingsConfig.CommonWindows
                .Concat(settingsConfig.TutorialWindows.Where(type => tutorialService.IsTutorialCompleted(type)));
            
            foreach (var type in types)
            {
                var elementButton = GetElementButton(type);
                contentPanel.Add(elementButton);
            }
        }

        private Button GetElementButton(WindowType type)
        {
            var element = settingsConfig.ListElement.Instantiate();
            var button = element.Q<Button>();
            button.text = GetWindowTitle(type);
                
            button.clicked += () =>
            {
                vibrationService.PlayVibration();
                screenDispatcher.ShowWindow(type);
            };

            return button;
        }

        private string GetWindowTitle(WindowType type)
        {
            var titleId = WindowsConfig.Windows[type].localizationData
                .First(data => data.element == WindowTitleName).textId;

            return LocalizationService.GetLocalizedText(titleId);
        }

        private void OnVibrationButtonClicked(ClickEvent _)
        {
            vibrationService.PlayVibration();
            settingsRepository.IsVibrationEnabled = !settingsRepository.IsVibrationEnabled;
            
            var vibrationOnPanel = WindowElement.Q<VisualElement>(VibrationOnPanelName);
            vibrationOnPanel.style.visibility = settingsRepository.IsVibrationEnabled ? Visibility.Visible : Visibility.Hidden;
        }
    }
}