using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Вью эффекта всплывающих циферок счета. Воспроизводит анимацию
    /// </summary>
    public class ScoreEffectView : MonoBehaviour
    {
        [Header("Animation")]
        [Tooltip("Дистанция движения циферок")]
        [SerializeField] private float moveDistance = 0.5f;
        
        [Tooltip("Изменение размера циферок в конце анимации")]
        [SerializeField] private float scaleModifier = 0.8f;
        
        [Tooltip("Длительность анимации")]
        [SerializeField] private float lifetime = 3f;
        
        [SerializeField] private TMP_Text text;

        public void Initialize(int score)
        {
            text.text = $"+{score.ToString()}";
            
            PlayAnimationAsync(destroyCancellationToken).Forget();
        }

        private async UniTaskVoid PlayAnimationAsync(CancellationToken token)
        {
            try
            {
                float halfLifetime = lifetime / 2;
            
                Sequence sequence = DOTween.Sequence();

                sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + moveDistance, lifetime));
                sequence.Join(text.DOFade(1f, halfLifetime));
                sequence.Insert(halfLifetime, text.DOFade(0f, halfLifetime));
                sequence.Join(transform.DOScale(scaleModifier, halfLifetime));

                await sequence.Play().WithCancellation(cancellationToken: token);
            
                Destroy(gameObject);
            }
            catch (OperationCanceledException _) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}