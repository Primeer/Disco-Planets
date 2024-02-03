using System;

namespace Boosters.Types
{
    public class AbstractBooster : IDisposable
    {
        private BoosterType type;
        
        protected int Index;

        public BoosterState State;
        public Action<BoosterType> Ready;
        public Action<BoosterType> Finished;

        
        public void Initialize(BoosterType type, int index)
        {
            this.type = type;
            Index = index;
        }

        public void Reset()
        {
            if (State == BoosterState.Active)
            {
                Deactivate();
            }
            
            State = BoosterState.Disabled;
        }

        public void Enable()
        {
            State = BoosterState.Ready;
            Ready?.Invoke(type);
        }
        
        public void Activate()
        {
            if (State == BoosterState.Ready)
            {
                State = BoosterState.Active;
                OnActivate();
            }
        }

        protected void Finish()
        {
            State = BoosterState.Disabled;
            Finished?.Invoke(type);
        }
        
        protected virtual void OnActivate() { }

        private void Deactivate()
        {
            OnDeactivate();
        }

        protected virtual void OnDeactivate() { }
        
        public void Dispose()
        {
            Finished = null;
        }
    }
}