using System;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using VContainer.Unity;
using View;

namespace Input
{
    /// <summary>
    /// Триггерит события тапов игроком по игровому полю
    /// </summary>
    public class GameInputProvider : IInitializable, IDisposable
    {
        private readonly GameInputView view;
        private readonly InputAction pointAction;
        
        private Camera mainCamera;
        private bool isPointerDown;
        private bool isEnabled;


        public GameInputProvider(GameInputView view, InputSystemUIInputModule uiInputModule)
        {
            isEnabled = true;
            this.view = view;
            pointAction = uiInputModule.actionsAsset.FindActionMap("UI").FindAction("Point");
        }
        
        public void EnableInput(bool enabled)
        {
            isEnabled = enabled;
        }
        
        public void Initialize()
        {
            mainCamera = Camera.main;
            
            view.PointerDown += OnPointerDown;
            view.PointerMove += OnPointerMove;
            view.PointerUp += OnPointerUp;
        }

        public void Dispose()
        {
            view.PointerDown -= OnPointerDown;
            view.PointerMove -= OnPointerMove;
            view.PointerUp -= OnPointerUp;
        }
        
        private void OnPointerDown(Vector2 screenPosition)
        {
            if (!isEnabled)
            {
                return;
            }
            
            isPointerDown = true;

            var position = GetPointerPosition();
            EventBus.PointerPressed?.Invoke(position);
        }
        
        private void OnPointerMove(Vector2 screenPosition)
        {
            if (!isEnabled || !isPointerDown)
            {
                return;
            }
            
            DebugService.Log(screenPosition.ToString());
            
            var position = GetPointerPosition();
            EventBus.PointerPressed?.Invoke(position);
        }

        private void OnPointerUp(Vector2 _)
        {
            if (!isEnabled || !isPointerDown)
            {
                return;
            }
            
            isPointerDown = false;
            EventBus.PointerReleased?.Invoke();
        }

        private Vector2 GetPointerPosition()
        {
            var position = pointAction.ReadValue<Vector2>();
            return mainCamera.ScreenToWorldPoint(position);
        }
    }
}