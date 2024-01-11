using Configs;
using UnityEngine;
using UnityEngine.Pool;
using View;
using Object = UnityEngine.Object;

namespace Service
{
    /// <summary>
    /// Спавнит шары
    /// </summary>
    public class BallFactory
    {
        private readonly Transform throwerBallRoot;
        private readonly ObjectPool<BallView> ballsPool;


        public BallFactory(GameObject ballPrefab, SceneContext sceneContext, CommonConfig commonConfig)
        {
            throwerBallRoot = sceneContext.ThrowerBallRoot;

            ballsPool = new ObjectPool<BallView>(
                createFunc: () => Object.Instantiate(ballPrefab, sceneContext.BallsRoot).GetComponent<BallView>(), 
                actionOnGet: (view) =>
                {
                    view.gameObject.SetActive(true);
                    view.Reset();
                }, 
                actionOnRelease: (view) => view.gameObject.SetActive(false), 
                actionOnDestroy: (view) => Object.Destroy(view.gameObject), 
                collectionCheck: false, 
                defaultCapacity: commonConfig.BallsLimit + 5, 
                maxSize: commonConfig.BallsLimit + 10);
        }
        
        public BallView CreateThrowerBall(int ballValue)
        {
            var ball = CreateBall(ballValue, Vector2.zero);
            var transform = ball.transform;
            transform.parent = throwerBallRoot;
            transform.localPosition = Vector2.zero;
            
            return ball;
        }

        public BallView CreateBall(int ballValue, Vector2 position)
        {
            BallView ballView = ballsPool.Get();
            ballView.transform.localPosition = position;
            
            EventBus.BallCreated?.Invoke(ballView, ballValue);
            
            return ballView;
        }

        public void DestroyBall(BallView view)
        {
            EventBus.BallDestroyed?.Invoke(view);
            ballsPool.Release(view);
        }
    }
}