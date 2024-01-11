using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Boosters.Types
{
    public class AbstractBooster
    {
        private CancellationTokenSource cooldownCts;
        private BoosterState state;
        
        protected int Index;

        public Action Ready;
        public Action Finished;

        
        public void Initialize(float cooldown, int idx)
        {
            Index = idx;
            state = BoosterState.Cooldown;
            
            cooldownCts?.Cancel();
            cooldownCts = new CancellationTokenSource();
            CooldownAsync(cooldown, cooldownCts.Token).Forget();
        }
        
        public void Activate()
        {
            if (state == BoosterState.Ready)
            {
                state = BoosterState.Active;
                OnActivate();
            }
        }

        protected void Finish()
        {
            cooldownCts?.Cancel();
            cooldownCts?.Dispose();
            cooldownCts = null;
            
            Finished?.Invoke();
            Finished = null;
        }

        private async UniTaskVoid CooldownAsync(float cooldown, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(cooldown), cancellationToken: token);
                
                if (state == BoosterState.Cooldown)
                {
                    state = BoosterState.Ready;
                    Ready?.Invoke();
                    Ready = null;
                }
            }
            catch (OperationCanceledException _) { }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        protected virtual void OnActivate() {}
    }
}