using System;
using UnityEngine.UIElements;

namespace Configs.Windows
{
    [Serializable]
    public class WindowData
    {
        public string name;
        public WindowType windowType;
        public VisualTreeAsset uxml;
        public WindowLocalizationData[] localizationData;
    }
}