using System;
using System.Threading;
using Configs;
using Configs.Levels;
using Cysharp.Threading.Tasks;
using Repository;
using Service;
using UnityEngine;
using View;
using Random = UnityEngine.Random;

namespace Model
{
    /// <summary>
    /// Модель, управляющая шаром в бросателе
    /// </summary>
    public class ThrowerModel
    {
        private readonly BallFactory ballFactory;
        private readonly LevelConfig levelConfig;
        private readonly ModifiersRepository modifiers;
        private readonly float spawnDelaySec;

        private CancellationTokenSource spawnDelayCts;
        private BallView ball;
        private int minValue;
        private int maxValue;

        public Action<BallView> BallCreated;

        public ThrowerModel(BallFactory ballFactory, LevelConfig levelConfig, 
            ModifiersRepository modifiers, CommonConfig config)
        {
            this.ballFactory = ballFactory;
            this.levelConfig = levelConfig;
            this.modifiers = modifiers;
            spawnDelaySec = config.ThrowerBallSpawnDelaySec;
        }

        public void OnLevelChanged(int level)
        {
            var levelData = levelConfig.GetLevelData(level);
            minValue = levelData.MinValue;
            maxValue = levelData.MaxThrowerValue;
            SpawnBall();
        }

        public void SetBall(BallView ballView)
        {
            if (ball != null)
            {
                ballFactory.DestroyBall(ball);
            }
            else
            {
                spawnDelayCts?.Cancel();
            }
            
            ball = ballView;
        }
        
        public void ThrowBall()
        {
            if (ball == null)
            {
                return;
            }
            
            EventBus.BallThrown?.Invoke(ball);
            ball = null;
            
            spawnDelayCts?.Cancel();
            spawnDelayCts = new CancellationTokenSource();
            DelayedSpawnBallAsync(spawnDelaySec, spawnDelayCts.Token).Forget();
        }
        
        public void SpawnBall()
        {
            if (ball != null)
            {
                ballFactory.DestroyBall(ball);
            }

            var ballValue = modifiers.IsFixValue
                ? maxValue + modifiers.FixValue - 1
                : Random.Range(minValue + modifiers.ValueMod, maxValue + modifiers.ValueMod);
            
            ball = ballFactory.CreateThrowerBall(ballValue);
            
            BallCreated?.Invoke(ball);
        }

        public void Dispose()
        {
            spawnDelayCts?.Cancel();
            spawnDelayCts?.Dispose();
            spawnDelayCts = null;
        }

        private async UniTask DelayedSpawnBallAsync(float delay, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

                SpawnBall();
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}