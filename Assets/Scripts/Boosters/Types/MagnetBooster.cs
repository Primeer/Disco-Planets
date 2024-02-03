using System;
using System.Linq;
using System.Threading;
using Boosters.Configs.Types;
using Cysharp.Threading.Tasks;
using Repository;
using Service;
using UnityEngine;
using VContainer;

namespace Boosters.Types
{
    /// <summary>
    /// Бустер "Магнит"
    /// </summary>
    public class MagnetBooster : AbstractBooster
    {
        [Inject] private readonly BallPhysicService physicService;
        [Inject] private readonly BallMergeService mergeService;
        [Inject] private readonly BallsRepository repository;
        [Inject] private readonly MagnetBoosterConfig config;
        [Inject] private readonly SceneContext sceneContext;

        private CancellationTokenSource cts;


        protected override void OnActivate()
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            ExplodeMergeAsync(cts.Token).Forget();
        }

        private async UniTaskVoid ExplodeMergeAsync(CancellationToken token)
        {
            try
            {
                physicService.Explode(sceneContext.GravitationPoint, config.Force);

                await UniTask.Delay(TimeSpan.FromSeconds(config.Delay), cancellationToken: token);
                
                Merge();
            }
            catch (OperationCanceledException _) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Dispose();
                Finish();
            }
        }

        private void Merge()
        {
            var balls = repository.Views.Where(v => v.IsSimulated).ToList();

            for (int i = 0; i < balls.Count - 1; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    mergeService.MergeIfPossible(balls[i], balls[j]);
                }
            }
        }

        private void Dispose()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }
    }
}