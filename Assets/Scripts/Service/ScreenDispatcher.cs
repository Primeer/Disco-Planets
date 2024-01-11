using System;
using Input;
using UnityEngine;
using View;

namespace Service
{
    /// <summary>
    /// Управляет отображением окон
    /// </summary>
    public class ScreenDispatcher
    {
        private readonly GameInputProvider inputProvider;
        private readonly WinWindow winWindow;

        
        public ScreenDispatcher(WinWindow winWindow, GameInputProvider inputProvider)
        {
            this.winWindow = winWindow;
            this.inputProvider = inputProvider;
        }

        public void ShowWinWindow(int score, int bestScore, bool withMessage, Action onResume = default)
        {
            inputProvider.EnableInput(false);
            Time.timeScale = 0;
            
            onResume += () =>  inputProvider.EnableInput(true);
            onResume += () => Time.timeScale = 1;
            
            winWindow.ShowWindow(score, bestScore, withMessage, onResume);
        }
    }
}