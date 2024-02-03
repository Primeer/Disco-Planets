using System;
using Configs.Levels;
using VContainer.Unity;

namespace Service.Features
{
    public class FeatureOpenService : IInitializable, IDisposable
    {
        private readonly LevelConfig levelConfig;

        public FeatureOpenService(LevelConfig levelConfig)
        {
            this.levelConfig = levelConfig;
        }

        public void OpenAllFeatures()
        {
            OpenFeature(FeatureEnum.BoosterMax);
            OpenFeature(FeatureEnum.BoosterBomb);
            OpenFeature(FeatureEnum.BoosterQuality);
            OpenFeature(FeatureEnum.BoosterMagnet);
        }

        public void OpenFeature(FeatureEnum feature)
        {
            // PlayerPrefs.SetInt(feature.ToString(), 1);  //пока нет смысла их сохранять
            EventBus.FeatureOpened?.Invoke(feature);
        }

        public void Initialize()
        {
            EventBus.LevelChanged += OnLevelChanged;
        }

        public void Dispose()
        {
            EventBus.LevelChanged -= OnLevelChanged;
        }

        private void OnLevelChanged(int level)
        {
            for (int i = 0; i <= level; i++)
            {
                var features = levelConfig.GetLevelData(i).Features;
                
                foreach (var feature in features)
                {
                    OpenFeature(feature);
                }
            }
        }
    }
}