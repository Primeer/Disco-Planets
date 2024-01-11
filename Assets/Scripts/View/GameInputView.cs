using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Вью игрового поля. Триггерит события тапа по полю
    /// </summary>
    public class GameInputView : MonoBehaviour
    {
        private const string PanelRootName = "panel_root";

        [SerializeField] private UIDocument document;

        private VisualElement rootPanel;

        public Action<Vector2> PointerDown;
        public Action<Vector2> PointerMove;
        public Action<Vector2> PointerUp;
        
        private void OnEnable()
        {
            var root = document.rootVisualElement;
            rootPanel = root.Q<VisualElement>(PanelRootName);
            
            rootPanel.RegisterCallback<PointerDownEvent>(OnPointerDown);
            rootPanel.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            rootPanel.RegisterCallback<PointerUpEvent>(OnPointerUp);
            rootPanel.RegisterCallback<PointerCancelEvent>(OnPointerCancel);
            rootPanel.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
        }

        private void OnPointerDown(PointerDownEvent e)
        {
            PointerDown?.Invoke(e.position);
        }

        private void OnPointerMove(PointerMoveEvent e)
        {
            PointerMove?.Invoke(e.position);
        }
        
        private void OnPointerUp(PointerUpEvent e)
        {
            PointerUp?.Invoke(e.position);
        }

        private void OnPointerCancel(PointerCancelEvent e)
        {
            PointerUp?.Invoke(e.position);
        }
        
        private void OnPointerLeave(PointerLeaveEvent e)
        {
            PointerUp?.Invoke(e.position);
        }
    }
}