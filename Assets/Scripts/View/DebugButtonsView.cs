using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class DebugButtonsView : MonoBehaviour
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        private const string PanelName = "panel_debug";
        private const string NextLevelButtonName = "button_debug_next_level";
        private const string ClearPrefsButtonName = "button_debug_clear_prefs";
        private const string DebugLabelName = "text_debug";
        private const string DebugFieldName = "field_debug";
        
        [SerializeField] private UIDocument document;

        public Action NextLevelButtonClicked;
        public Action ClearPrefsButtonClicked;

        private static Label debugLabel;
        private static IntegerField debugField;

        public static void SetDebugText(string text)
        {
            debugLabel.text = text;
        }
        
        public static int GetDebugValue()
        {
            return debugField.value;
        }

        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            var panel = root.Q<VisualElement>(PanelName);
            panel.style.display = DisplayStyle.Flex;
            
            debugLabel = root.Q<Label>(DebugLabelName);
            debugField = root.Q<IntegerField>(DebugFieldName);
            
            var button = root.Q<Button>(NextLevelButtonName);
            button.RegisterCallback<ClickEvent>(OnNextLevelButtonClick);
            
            var clearPrefsButton = root.Q<Button>(ClearPrefsButtonName);
            clearPrefsButton.RegisterCallback<ClickEvent>(OnClearPrefsButtonClick);
        }
        
        private void OnNextLevelButtonClick(ClickEvent _)
        {
            NextLevelButtonClicked?.Invoke();
        }
        
        private void OnClearPrefsButtonClick(ClickEvent _)
        {
            ClearPrefsButtonClicked?.Invoke();
        }
#endif
    }
}