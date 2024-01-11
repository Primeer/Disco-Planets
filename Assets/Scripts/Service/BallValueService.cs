using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.Levels;
using Repository;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Service
{
    /// <summary>
    /// Позволяет взаимодействовать со значениями шаров
    /// </summary>
    public class BallValueService : IInitializable, IDisposable
    {
        private readonly BallsRepository repository;
        private readonly BallValuesConfig valuesConfig;
        private readonly EffectFactory effectFactory;
        private readonly BallFactory ballFactory;
        private readonly LevelConfig levelConfig;

        public BallValueService(BallsRepository repository, BallValuesConfig valuesConfig, EffectFactory effectFactory, 
            BallFactory ballFactory, LevelConfig levelConfig)
        {
            this.repository = repository;
            this.valuesConfig = valuesConfig;
            this.effectFactory = effectFactory;
            this.ballFactory = ballFactory;
            this.levelConfig = levelConfig;
        }
        
        public void Initialize()
        {
            EventBus.BallCreated += OnBallCreated;
            EventBus.LevelChanged += OnLevelChanged;
        }

        public void Dispose()
        {
            EventBus.BallCreated -= OnBallCreated;
            EventBus.LevelChanged -= OnLevelChanged;
        }

        private void OnBallCreated(BallView view, int value)
        {
            SetBallValue(view, value);
        }

        private void OnLevelChanged(int level)
        {
            int minValue = levelConfig.GetLevelData(level).MinValue;
            DeleteBallsUnderValue(minValue);
        }

        public void IncreaseValue(int deltaValue, Vector2 position, float radius = Mathf.Infinity)
        {
            if (float.IsPositiveInfinity(radius))
            {
                foreach (var view in repository.Views)
                {
                    IncreaseValue(view, deltaValue);
                }
            }
            else
            {
                List<BallView> views = repository.GetBallsInArea(position, radius);
                
                foreach (var view in views)
                {
                    IncreaseValue(view, deltaValue);
                }
            }
        }
        
        private void IncreaseValue(BallView view, int deltaValue)
        {
            int newValue = repository.Values[view] + deltaValue;
                    
            SetBallValue(view, newValue);
            repository.Values[view] = newValue;
            effectFactory.CreateMergeEffect(view.Position);
        }
        
        public void SetBallValue(BallView view, int value)
        {
            if (view.HasCustomValue)
            {
                return;
            }
            
            var valueData = valuesConfig.Values[value];
            
            Sprite sprite = valueData.sprite;
            float scale = valueData.scale;
            float weight = valueData.weigh;
            
            view.SetSprite(sprite);
            view.SetScale(scale);
            view.SetWeight(weight);
        }

        private void DeleteBallsUnderValue(int value)   
        {
            Dictionary<BallView, int> balls = repository.Values.Where(pair => pair.Key.IsSimulated).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var pair in balls)
            {
                if (pair.Value < value)
                {
                    ballFactory.DestroyBall(pair.Key);
                }
            }
        }
    }
}