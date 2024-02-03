using System;
using Configs.Windows;
using Service;
using UnityEngine.UIElements;
using VContainer;
using View.Windows;

namespace Presenter.Windows
{
    public class WindowPresenter
    {
        [Inject] protected readonly WindowsConfig WindowsConfig;
        [Inject] protected readonly LocalizationService LocalizationService;

        protected ScreenDispatcher Dispatcher;
        protected WindowView View;
        
        public VisualElement WindowElement;
        public Action ContinueButtonClicked;
        public Action OnResume;

        public WindowPresenter() : this(new WindowView()) { }

        public WindowPresenter(WindowView view)
        {
            View = view;
        }

        public void Initialize(WindowType type, VisualElement root, ScreenDispatcher screenDispatcher)
        {
            Dispatcher = screenDispatcher;
            WindowElement = WindowsConfig.Windows[type].uxml.Instantiate();
            root.Add(WindowElement);
            LocalizeWindow(WindowElement, type);
            View.Initialize(WindowElement);
        }
        
        public void Show()
        {
            View.ContinueButtonClicked += OnContinueButtonClicked;
            View.Visible = true;
            OnShow();
        }
        
        public void Hide()
        {
            View.ContinueButtonClicked -= OnContinueButtonClicked;
            View.Visible = false;
            OnHide();
        }
        
        protected virtual void OnShow() { }
        
        protected virtual void OnHide() { }
        
        private void LocalizeWindow(VisualElement window, WindowType type)
        {
            foreach (var data in WindowsConfig.Windows[type].localizationData)
            {
                window.Q<TextElement>(data.element).text = LocalizationService.GetLocalizedText(data.textId);
            }
        }

        private void OnContinueButtonClicked()
        {
            Dispatcher.CloseWindow(this);
            OnResume?.Invoke();
            ContinueButtonClicked?.Invoke();
        }
    }
}