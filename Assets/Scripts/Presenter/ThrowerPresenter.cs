using System;
using Model;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    /// <summary>
    /// Меняет положение бросателя при тапах игрока по полю
    /// </summary>
    public class ThrowerPresenter : IInitializable, IDisposable
    {
        private readonly ThrowerModel model;
        private readonly ThrowerView view;
        
        
        public ThrowerPresenter(ThrowerModel model, ThrowerView view)
        {
            this.model = model;
            this.view = view;
        }
        
        public void Initialize()
        {
            EventBus.PointerPressed += OnPointerPressed;
            EventBus.PointerReleased += OnPointerReleased;
            EventBus.LevelChanged += OnLevelChanged;
            EventBus.GamePaused += OnGamePaused;
        }

        public void Dispose()
        {
            EventBus.PointerPressed -= OnPointerPressed;
            EventBus.PointerReleased -= OnPointerReleased;
            EventBus.LevelChanged -= OnLevelChanged;
            EventBus.GamePaused -= OnGamePaused;
            
            model.Dispose();
        }

        private void OnGamePaused()
        {
            view.SetAimLine(false);
        }

        private void OnPointerPressed(Vector2 position)
        {
            view.SetPosition(position.x);
            view.SetAimLine(true);
        }

        private void OnPointerReleased()
        {
            model.ThrowBall();
            view.SetAimLine(false);
            view.ResetPosition();
        }

        private void OnLevelChanged(int level)
        {
            model.OnLevelChanged(level);
        }
    }
}