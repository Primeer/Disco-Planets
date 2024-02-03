using UnityEngine;
using UnityEngine.UIElements;

namespace Configs
{
    [CreateAssetMenu(fileName = "DebugConfig", menuName = "Scriptables/DebugConfig", order = 0)]
    public class DebugConfig : ScriptableObject
    {
        public VisualTreeAsset gameElementsUxml;
        public VisualTreeAsset logWindowUxml;
    }
}