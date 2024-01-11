using System.Linq;
using Localization;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    /// <summary>
    /// Дает доступ к локализованным строкам
    /// </summary>
    public class LocalizationService : IInitializable
    {
        private readonly LocalizationData localizationData;

        private SystemLanguage systemLanguage;

        
        public LocalizationService(LocalizationData localizationData)
        {
            this.localizationData = localizationData;
        }

        public void Initialize()
        {
            systemLanguage = Application.systemLanguage;
        }

        public string GetLocalizedText(int id)
        {
            var data = GetLocalizationString(id);

            return systemLanguage switch
            {
                SystemLanguage.Russian => data.Ru,
                _ => data.En
            };
        }
        
        public string GetLocalizedTextWithParams(int id, params object[] parameters)
        {
            var data = GetLocalizationString(id);

            return systemLanguage switch
            {
                SystemLanguage.Russian => string.Format(data.Ru, parameters),
                _ => string.Format(data.En, parameters)
            };
        }

        private LocalizationString GetLocalizationString(int id)
        {
            return localizationData.Strings.First(s => s.Id == id);
        }
    }
}