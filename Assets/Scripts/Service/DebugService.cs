using UnityEngine;
using View;

namespace Service
{
    public class DebugService
    {
        public static void Log(string message, Object source = null)
        {
            string text = source == null ? $"{message}" : $"{source}: {message}";
            Debug.Log(text);
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            DebugView.SetDebugText(message);
#endif
        }

        public static int GetDebugValue()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            return DebugView.GetDebugValue();
#endif
            return -1;
        }
    }
}