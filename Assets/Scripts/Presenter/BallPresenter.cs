using System;
using Model;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class BallPresenter : IInitializable, IDisposable
    {
        private readonly BallModel model;
        

        public BallPresenter(BallModel model)
        {
            this.model = model;
        }

        public void Initialize()
        {
            EventBus.BallThrown += OnBallThrown;
        }

        public void Dispose()
        {
            EventBus.BallThrown -= OnBallThrown;
        }

        private void OnBallThrown(BallView view)
        {
            model.OnThrow(view);
        }
    }
}