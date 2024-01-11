using System;
using System.Collections.Generic;
using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View;

namespace Model
{
    /// <summary>
    /// Модель управления подсчетом количества шаров на поле.
    /// При превышении лимита запускает таймер, по окончании которого игрок проигрывает.
    /// </summary>
    public class BallsLimitModel
    {
        private readonly int limitCount;
        private readonly float timerTime;

        private float currentTime;
        private CancellationTokenSource cts;
        private bool timerEnabled;
        
        public Action<int, int> CountChanged;
        public Action<float, float> TimerChanged;
        public Action TimerDisabled;

        
        public BallsLimitModel(CommonConfig config)
        {
            limitCount = config.BallsLimit;
            timerTime = config.GameOverTimer;
        }

        public void OnBallsChanged(List<BallView> balls)
        {
            var count = balls.Count;
            CountChanged?.Invoke(count, limitCount);

            if (count > limitCount)
            {
                if (!timerEnabled)
                {
                    timerEnabled = true;
                    
                    cts?.Cancel();
                    cts = new CancellationTokenSource();
                    EnableTimer(cts.Token).Forget();
                }
            }
            else
            {
                cts?.Cancel();
            }
        }

        public void Dispose()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }

        private async UniTaskVoid EnableTimer(CancellationToken token)
        {
            currentTime = timerTime;

            try
            {
                while (currentTime > 0)
                {
                    TimerChanged?.Invoke(currentTime, timerTime);
                    currentTime -= Time.deltaTime;
                    await UniTask.Yield(cancellationToken: token);
                }

                Debug.Log("Time is over"); //todo: прикрутить функционал поражения
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                timerEnabled = false;
                TimerDisabled?.Invoke();
            }
        }
    }
}