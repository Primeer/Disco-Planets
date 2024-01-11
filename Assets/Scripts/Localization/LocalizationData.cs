using UnityEngine;

namespace Localization
{
    /// <summary>
    /// Конфиг с локлизазионными строками
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Scriptables/LocalizationData", order = 0)]
    public class LocalizationData : ScriptableObject
    {
        [SerializeField] private LocalizationString[] strings;
        
        public LocalizationString[] Strings => strings;

        private void OnValidate()
        {
            strings[^1].Id = strings.Length - 1;
        }
    }
}