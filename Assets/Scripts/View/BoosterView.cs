using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вью кнопок бустеров
    /// </summary>
    public class BoosterView : MonoBehaviour
    {
        private const string PanelName = "panel_booster";
        private const string ButtonName = "button_booster";
        private const string PanelActivatedClass = "panel-booster-activated";
        private const string PanelDisabledClass = "panel-booster-disabled";
        private const string ButtonActivatedClass = "button-booster-activated";
        private const string ButtonDisabledClass = "button-booster-disabled";
        private const string ButtonAdClass = "button-booster-ad";
        
        [SerializeField] private UIDocument document;

        private VisualElement[] panels;
        private Button[] buttons;

        public Action<int> ButtonClicked;
        
        public void ShowBooster(int index, bool isVisible)
        {
            panels[index].style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void SetButtonEnabled(int index, bool isEnabled)
        {
            var button = buttons[index];
            button.SetEnabled(isEnabled);

            var state = isEnabled ? ElementState.None : ElementState.Disabled;
            SetPanelState(index, state);
            SetButtonState(index, state);
        }
        
        public void SetButtonSprite(int index, Sprite sprite)
        {
            buttons[index].style.backgroundImage = new StyleBackground(sprite);
        }
        
        public void SetButtonText(int index, string text)
        {
            buttons[index].text = text;
        }

        public void SetAd(int index, bool isEnabled)
        {
            var state = isEnabled ? ElementState.Ad : ElementState.None;
            SetPanelState(index, state);
            SetButtonState(index, state);
            SetButtonText(index, isEnabled ? "ad" : "");
        }

        public void SetBoosterActivated(int index, bool isActive)
        {
            var state = isActive ? ElementState.Activated : ElementState.None;
            SetButtonState(index, state);
            SetPanelState(index, state);
        }

        private void SetButtonState(int index, ElementState state)
        {
            var button = buttons[index];
            
            switch (state)
            {
                case ElementState.None:
                    button.RemoveFromClassList(ButtonActivatedClass);
                    button.RemoveFromClassList(ButtonDisabledClass);
                    button.RemoveFromClassList(ButtonAdClass);
                    break;
                case ElementState.Disabled:
                    button.AddToClassList(ButtonDisabledClass);
                    button.RemoveFromClassList(ButtonActivatedClass);
                    button.RemoveFromClassList(ButtonAdClass);
                    break;
                case ElementState.Activated:
                    button.AddToClassList(ButtonActivatedClass);
                    button.RemoveFromClassList(ButtonDisabledClass);
                    button.RemoveFromClassList(ButtonAdClass);
                    break;
                case ElementState.Ad:
                    button.AddToClassList(ButtonAdClass);
                    button.RemoveFromClassList(ButtonActivatedClass);
                    button.RemoveFromClassList(ButtonDisabledClass);
                    break;
            }
        }
        
        private void SetPanelState(int index, ElementState state)
        {
            var panel = panels[index];
            
            switch (state)
            {
                case ElementState.None:
                    panel.RemoveFromClassList(PanelActivatedClass);
                    panel.RemoveFromClassList(PanelDisabledClass);
                    break;
                case ElementState.Ad:
                case ElementState.Disabled:
                    panel.AddToClassList(PanelDisabledClass);
                    panel.RemoveFromClassList(PanelActivatedClass);
                    break;
                case ElementState.Activated:
                    panel.AddToClassList(PanelActivatedClass);
                    panel.RemoveFromClassList(PanelDisabledClass);
                    break;
            }
        }

        private void HideBoosters()
        {
            panels.ToList().ForEach(panel => panel.style.display = DisplayStyle.None);
        }

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;
            
            panels = root.Query<VisualElement>(PanelName).Build().ToArray();
            buttons = root.Query<Button>(ButtonName).Build().ToArray();

            for (var i = 0; i < buttons.Length; i++)
            {
                var index = i;
                buttons[i].RegisterCallback<ClickEvent>(e => ButtonClicked?.Invoke(index));
            }
            
            HideBoosters();
        }
    }

    public enum ElementState
    {
        None,
        Disabled,
        Activated,
        Ad,
    }
}