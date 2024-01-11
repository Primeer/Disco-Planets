using System;

namespace Localization
{
    /// <summary>
    /// Данные локализационной строки
    /// </summary>
    [Serializable]
    public class LocalizationString
    {
        public int Id;
        public string Ru;
        public string En;
    }
}