using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вьюха кнопки включения вибрации
    /// </summary>
    public class VibrationButtonView : MonoBehaviour
    {
        private const string ButtonName = "button_vibration";
        private const string ButtonOnStyleName = "toggle-vibration-on";
        
        [SerializeField] private UIDocument document;

        private Button button;
        private bool state;

        public Action<bool> VibrationButtonClicked;
        
        
        private void OnEnable()
        {
            var root = document.rootVisualElement;
            
            button = root.Q<Button>(ButtonName);
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
        }
        
        private void OnButtonClicked(ClickEvent _)
        {
            state = !state;
            
            if (state)
            {
                button.AddToClassList(ButtonOnStyleName);
            }
            else
            {
                button.RemoveFromClassList(ButtonOnStyleName);
            }
            
            VibrationButtonClicked?.Invoke(state);
        }
    }
}