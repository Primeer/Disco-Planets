using System;
using Configs.Levels;
using Model;
using Repository;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Service
{
    /// <summary>
    /// Обрабатывет столкновения и мерж шаров
    /// </summary>
    public class BallMergeService : IInitializable, IDisposable
    {
        private readonly BallFactory ballFactory;
        private readonly EffectFactory effectFactory;
        private readonly BallsRepository repository;
        private readonly BallModel ballModel;
        private readonly LevelConfig levelConfig;
        private readonly BallView mainBall;
        private readonly VibrationService vibrationService;

        private int maxBallValue;
        

        public BallMergeService(BallsRepository repository, BallFactory ballFactory, EffectFactory effectFactory, 
            BallModel ballModel, LevelConfig levelConfig, SceneContext sceneContext, VibrationService vibrationService)
        {
            this.repository = repository;
            this.ballFactory = ballFactory;
            this.effectFactory = effectFactory;
            this.ballModel = ballModel;
            this.levelConfig = levelConfig;
            this.vibrationService = vibrationService;
            mainBall = sceneContext.MainBall;
        }
        
        public void Initialize()
        {
            EventBus.BallCreated += OnBallCreated;
            EventBus.LevelChanged += OnLevelChanged;
            mainBall.BallsCollided += OnMainBallCollided;
        }

        public void Dispose()
        {
            EventBus.BallCreated -= OnBallCreated;
            EventBus.LevelChanged -= OnLevelChanged;
            mainBall.BallsCollided -= OnMainBallCollided;
        }

        private void OnLevelChanged(int level)
        {
            maxBallValue = levelConfig.GetLevelData(level).MaxValue;
        }

        private void OnBallCreated(BallView view, int _)
        {
            view.BallsCollided += OnBallsCollided;
        }

        private void OnBallsCollided(BallView ball1, BallView ball2)
        {
            MergeIfPossible(ball1, ball2);
        }

        private void OnMainBallCollided(BallView ball1, BallView ball2)
        {
            if (!repository.Values.TryGetValue(ball2, out int value))
            {
                return;
            }
            
            if (value == maxBallValue)
            {
                Debug.Log($"Main ball merged: {value}");
                effectFactory.CreateMergeEffect(ball2.transform.position);
                ballFactory.DestroyBall(ball2);
                EventBus.MainBallMerged?.Invoke();
            }
        }

        public void MergeIfPossible(BallView ball1, BallView ball2)
        {
            if (!repository.Values.TryGetValue(ball1, out int value1) 
                || !repository.Values.TryGetValue(ball2, out int value2))
            {
                return;
            }
            
            if (value1 != value2 || value1 >= maxBallValue || value2 >= maxBallValue)
            {
                return;
            }
            
            ball1.MarkAsInCollision();
            ball2.MarkAsInCollision();
                
            Vector2 avgPosition = GetAvgPosition(ball1, ball2);
            Vector2 avgVelocity = GetAvgVelocity(ball1, ball2);

            vibrationService.PlayVibration();
            effectFactory.CreateMergeEffect(avgPosition);
                
            ballFactory.DestroyBall(ball1);
            ballFactory.DestroyBall(ball2);

            int value = value1 + 1;
            var ball = ballFactory.CreateBall(value, avgPosition);
            ballModel.EnablePhysic(ball, avgVelocity);

            EventBus.BallsMerged?.Invoke(value, avgPosition);
        }

        private Vector2 GetAvgPosition(BallView view1, BallView view2)
        {
            Vector2 position1 = view1.transform.localPosition;
            Vector2 position2 = view2.transform.localPosition;
            return MathUtils.AverageVector2(position1, position2);
        }
        
        private Vector2 GetAvgVelocity(BallView view1, BallView view2)
        {
            Vector2 velocity1 = view1.Velocity;
            Vector2 velocity2 = view2.Velocity;
            return MathUtils.AverageVector2(velocity1, velocity2);
        }
    }
}