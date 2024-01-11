using Boosters.Configs.Types;
using Model;
using Service;
using UnityEngine;
using VContainer;
using View;

namespace Boosters.Types
{
    /// <summary>
    /// Бустер "Бомба"
    /// </summary>
    public class BombBooster : AbstractBooster
    {
        [Inject] private readonly BombBoosterConfig config;
        [Inject] private readonly ThrowerModel throwerModel;
        [Inject] private readonly SceneContext sceneContext;
        [Inject] private readonly BallPhysicService physicService;
        [Inject] private readonly BallValueService valueService;
        [Inject] private readonly EffectFactory effectFactory;


        protected override void OnActivate()
        {
            var bomb = Object.Instantiate(config.BombPrefab, sceneContext.ThrowerBallRoot).GetComponent<BallView>();
            throwerModel.SetBall(bomb);

            bomb.BallsCollided += OnBombCollided;
        }

        private void OnBombCollided(BallView bomb, BallView _)
        {
            Vector2 position = bomb.Position;
            Object.Destroy(bomb.gameObject);
            effectFactory.CreateEffect(config.BombEffectPrefab, position);

            physicService.Explode(position, config.Force, config.Radius);
            valueService.IncreaseValue(config.Value, position, config.Radius);
            
            Finish();
        }
    }
}