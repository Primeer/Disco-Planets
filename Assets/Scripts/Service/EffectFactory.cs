using UnityEngine;
using View;

namespace Service
{
    /// <summary>
    /// Спавнит эффекты на игровом поле
    /// </summary>
    public class EffectFactory
    {
        private readonly GameObject mergeEffectPrefab;
        private readonly GameObject scoreEffectPrefab;
        private readonly Transform effectRoot;

        
        public EffectFactory(SceneContext sceneContext, GameObject mergeEffectPrefab, GameObject scoreEffectPrefab)
        {
            effectRoot = sceneContext.EffectsRoot;
            this.mergeEffectPrefab = mergeEffectPrefab;
            this.scoreEffectPrefab = scoreEffectPrefab;
        }

        public void CreateEffect(GameObject prefab, Vector2 position)
        {
            GameObject effect = Object.Instantiate(prefab, effectRoot);
            effect.transform.localPosition = position;
        }

        public void CreateMergeEffect(Vector2 position)
        {
            GameObject effect = Object.Instantiate(mergeEffectPrefab, effectRoot);
            effect.transform.localPosition = position;
        }
        
        public void CreateScoreEffect(int score, Vector2 position)
        {
            ScoreEffectView effectView = Object.Instantiate(scoreEffectPrefab, effectRoot).GetComponent<ScoreEffectView>();
            effectView.transform.localPosition = MathUtils.RandomPositionInHalfCircle(position, 0.5f);
            effectView.Initialize(score);
        }
    }
}