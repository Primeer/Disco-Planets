using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class DebugView : MonoBehaviour
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        private const string PanelName = "panel_debug";
        private const string NextLevelButtonName = "button_debug_next_level";
        private const string ClearPrefsButtonName = "button_debug_clear_prefs";
        private const string LogsButtonName = "button_debug_logs";
        private const string FeaturesButtonName = "button_debug_features";
        private const string DebugLabelName = "text_debug";
        private const string DebugFieldName = "field_debug";
        
        [SerializeField] private UIDocument document;

        private Foldout foldout;

        public Action NextLevelButtonClicked;
        public Action ClearPrefsButtonClicked;
        public Action LogsButtonClicked;
        public Action FeaturesButtonClicked;

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

        public void SpawnElements(VisualTreeAsset elementsUxml)
        {
            var elements = elementsUxml.Instantiate();
            foldout.Add(elements);
            
            debugLabel = elements.Q<Label>(DebugLabelName);
            debugField = elements.Q<IntegerField>(DebugFieldName);
            
            var button = elements.Q<Button>(NextLevelButtonName);
            button.RegisterCallback<ClickEvent>(OnNextLevelButtonClick);
            
            var clearPrefsButton = elements.Q<Button>(ClearPrefsButtonName);
            clearPrefsButton.RegisterCallback<ClickEvent>(OnClearPrefsButtonClick);
            
            var logsButton = elements.Q<Button>(LogsButtonName);
            logsButton.RegisterCallback<ClickEvent>(OnLogsButtonClick);
            
            var featuresButton = elements.Q<Button>(FeaturesButtonName);
            featuresButton.RegisterCallback<ClickEvent>(OnFeaturesButtonClick);
        }

        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            var panel = root.Q<VisualElement>(PanelName);
            panel.style.display = DisplayStyle.Flex;
            
            foldout = root.Q<Foldout>("foldout");
        }
        
        private void OnNextLevelButtonClick(ClickEvent _)
        {
            NextLevelButtonClicked?.Invoke();
        }
        
        private void OnClearPrefsButtonClick(ClickEvent _)
        {
            ClearPrefsButtonClicked?.Invoke();
        }
        
        private void OnLogsButtonClick(ClickEvent _)
        {
            LogsButtonClicked?.Invoke();
        }
        
        private void OnFeaturesButtonClick(ClickEvent _)
        {
            FeaturesButtonClicked?.Invoke();
        }
#endif
    }
}