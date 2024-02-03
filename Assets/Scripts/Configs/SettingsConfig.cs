using Configs.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace Configs
{
    [CreateAssetMenu(fileName = "SettingsConfig", menuName = "Scriptables/SettingsConfig", order = 0)]
    public class SettingsConfig : ScriptableObject
    {
        [SerializeField] private VisualTreeAsset listElement;
        [SerializeField] private WindowType[] commonWindows;
        [SerializeField] private WindowType[] tutorialWindows;
        
        public VisualTreeAsset ListElement => listElement;
        public WindowType[] CommonWindows => commonWindows;
        public WindowType[] TutorialWindows => tutorialWindows;
    }
}