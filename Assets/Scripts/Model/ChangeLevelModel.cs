using System;
using System.Threading;
using Configs.Levels;
using Cysharp.Threading.Tasks;
using Service;

namespace Model
{
    /// <summary>
    /// Модель управления эффектами, которые воспроизводятся при мерже центрального шара
    /// </summary>
    public class ChangeLevelModel
    {
        private readonly SceneContext sceneContext;
        private readonly BallPhysicService physicService;
        private readonly EffectFactory effectFactory;
        private readonly ChangeLevelEffectsConfig config;

        public ChangeLevelModel(BallPhysicService physicService, SceneContext sceneContext, EffectFactory effectFactory, 
            ChangeLevelEffectsConfig config)
        {
            this.physicService = physicService;
            this.sceneContext = sceneContext;
            this.effectFactory = effectFactory;
            this.config = config;
        }

        public async UniTask PlayLevelEndEffectsAsync(CancellationToken token)
        {
            effectFactory.CreateMergeEffect(sceneContext.GravitationPoint);
            physicService.Explode(sceneContext.GravitationPoint, config.ExplosionsForce);
            await UniTask.Delay(TimeSpan.FromSeconds(config.NextLevelWindowDelay), cancellationToken: token);
        }
    }
}