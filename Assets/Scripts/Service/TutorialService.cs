using System.Collections.Generic;
using Boosters;
using Configs.Windows;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    public class TutorialService : IStartable
    {
        private const string MergeTutorialSaveKey = "merge_tutorial";
        
        private readonly ScreenDispatcher screenDispatcher;
        private readonly Dictionary<string, bool> completedTutorials = new();

        public TutorialService(ScreenDispatcher screenDispatcher)
        {
            this.screenDispatcher = screenDispatcher;
        }

        public void Start()
        {
            ShowMergeTutorial();
        }

        public void ShowBoosterTutorial(WindowType type)
        {
            var key = type.ToString();
            
            if (IsTutorialCompleted(key))
            {
                return;
            }

            Debug.Log($"Show booster tutorial: {key}");
            SetTutorialCompleted(key);

            screenDispatcher.ShowWindow(type);
        }

        private void ShowMergeTutorial()
        {
            if (IsTutorialCompleted(MergeTutorialSaveKey))
            {
                return;
            }

            Debug.Log($"Show tutorial: {MergeTutorialSaveKey}");
            SetTutorialCompleted(MergeTutorialSaveKey);
            screenDispatcher.ShowWindow(WindowType.MergeTutorial);
        }

        public bool IsTutorialCompleted(WindowType windowType)
        {
            return IsTutorialCompleted(windowType.ToString());
        }

        private bool IsTutorialCompleted(string key)
        {
            if (!completedTutorials.ContainsKey(key))
            {
                completedTutorials[key] = PlayerPrefs.GetInt(key, 0) == 1;
            }

            return completedTutorials[key];
        }

        private void SetTutorialCompleted(string key)
        {
            completedTutorials[key] = true;
            PlayerPrefs.SetInt(key, 1);
        }
    }
}