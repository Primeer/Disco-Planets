using System;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class DebugButtonsPresenter : IInitializable, IDisposable
    {
        private readonly DebugButtonsView view;
        

        public DebugButtonsPresenter(DebugButtonsView view)
        {
            this.view = view;
        }

        public void Initialize()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            view.NextLevelButtonClicked += OnNextLevelButtonClicked;
            view.ClearPrefsButtonClicked += OnClearPrefsButtonClick;
#endif
        }

        public void Dispose()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            view.NextLevelButtonClicked -= OnNextLevelButtonClicked;
            view.ClearPrefsButtonClicked -= OnClearPrefsButtonClick;
#endif
        }

        private void OnNextLevelButtonClicked()
        {
            EventBus.MainBallMerged?.Invoke();
        }
        
        private void OnClearPrefsButtonClick()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}