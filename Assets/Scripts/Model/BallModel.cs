using System;
using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View;

namespace Model
{
    /// <summary>
    /// Модель управления шарами в полете
    /// </summary>
    public class BallModel
    {
        private readonly SceneContext sceneContext;
        private readonly Vector2 gravitationPoint;
        private readonly float startSpeed;
        private readonly float gravitationForce;
        private readonly float freeFallTime;
        private readonly float dragIncreaseRatio;
        private readonly float maxDrag;

        
        public BallModel(BallFlightConfig flightConfig, SceneContext sceneContext)
        {
            this.sceneContext = sceneContext;
            gravitationForce = flightConfig.GravitationForce;
            startSpeed = flightConfig.StartSpeed;
            freeFallTime = flightConfig.FreeFallDistance / startSpeed;
            dragIncreaseRatio = flightConfig.MaxDrag / freeFallTime;
            maxDrag = flightConfig.MaxDrag;
            gravitationPoint = sceneContext.GravitationPoint;
        }
        
        public void EnablePhysic(BallView view, Vector2 velocity)
        {
            view.EnableSimulation(true);
            view.SetVelocity(velocity);
            view.SetGravitation(true, gravitationPoint, gravitationForce);
            view.SetDrag(maxDrag);
        }
        
        public void OnThrow(BallView view)
        {
            MoveToRoot(view);
            
            var ballVelocity = Vector2.up * startSpeed;
            view.OnThrow(ballVelocity, dragIncreaseRatio);
            
            EnableGravitationAsync(view, freeFallTime, view.destroyCancellationToken).Forget();
        }
        
        private async UniTaskVoid EnableGravitationAsync(BallView view, float delay, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            
                view.SetGravitation(true, gravitationPoint, gravitationForce);
                view.SetDrag(maxDrag);
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void MoveToRoot(BallView view)
        {
            view.transform.parent = sceneContext.BallsRoot;
        }
    }
}