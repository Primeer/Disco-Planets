using System.Collections.Generic;
using System.Linq;
using Presenter;
using Presenter.Windows;
using UnityEngine;
using UnityEngine.UIElements;
using View.Windows;

namespace Configs.Windows
{
    [CreateAssetMenu(fileName = "Windows Config", menuName = "Scriptables/Windows Config", order = 0)]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private WindowData[] windows;

        public Dictionary<WindowType, WindowData> Windows => windows.ToDictionary(
            window => window.windowType,
            window => window);

        public Dictionary<WindowType, WindowPresenter> Presenters = new ()
        {
            { WindowType.Win, new WinWindowPresenter(new WinWindowView()) },
            { WindowType.Logs , new LogsWindowPresenter()},
            { WindowType.Settings , new SettingsWindowPresenter()},
        };

        private void OnValidate()
        {
            foreach (var window in windows)
            {
                window.name = window.windowType.ToString();
                
                if (window.localizationData.Length != 0 || window.uxml == null)
                {
                    continue;
                }
                
                var ve = window.uxml.Instantiate();
                    
                var labelData = ve.Query<Label>().Build()
                    .Where(label => label.name != "")
                    .Select(label => new WindowLocalizationData {element = label.name})
                    .ToArray();
                    
                var buttonData = ve.Query<Button>().Build()
                    .Where(button => button.name != "")
                    .Select(button => new WindowLocalizationData {element = button.name})
                    .ToArray();

                window.localizationData = labelData.Concat(buttonData).ToArray();
            }
        }
    }
}