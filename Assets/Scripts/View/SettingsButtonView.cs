using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class SettingsButtonView : MonoBehaviour
    {
        private const string ButtonName = "button_settings";
        
        [SerializeField] private UIDocument document;

        private Button button;

        public Action ButtonClicked;
        
        
        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            button = root.Q<Button>(ButtonName);
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }
        
        private void OnButtonClicked(ClickEvent _)
        {
            ButtonClicked?.Invoke();
        }
    }
}