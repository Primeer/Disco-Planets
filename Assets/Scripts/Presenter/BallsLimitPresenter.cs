using System;
using Model;
using Repository;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class BallsLimitPresenter : IInitializable, IDisposable
    {
        private readonly BallsLimitView view;
        private readonly TimerView timerView;
        private readonly BallsLimitModel model;
        private readonly BallsRepository repository;

        
        public BallsLimitPresenter(BallsLimitModel model, BallsRepository repository, BallsLimitView view, 
            TimerView timerView)
        {
            this.model = model;
            this.repository = repository;
            this.view = view;
            this.timerView = timerView;
        }
        
        public void Initialize()
        {
            repository.BallsChanged += model.OnBallsChanged;
            model.CountChanged += OnCountChanged;
            model.TimerChanged += OnTimerChanged;
            model.TimerDisabled += OnTimerDisabled;
        }

        public void Dispose()
        {
            repository.BallsChanged -= model.OnBallsChanged;
            model.CountChanged -= OnCountChanged;
            model.TimerChanged -= OnTimerChanged;
            model.TimerDisabled -= OnTimerDisabled;
            
            model.Dispose();
        }

        private void OnCountChanged(int current, int limit)
        {
            view.SetProgress(current, limit);
        }

        private void OnTimerChanged(float currentTime, float maxTime)
        {
            timerView.SetTimer(true, currentTime, maxTime);
        }

        private void OnTimerDisabled()
        {
            timerView.SetTimer(false);
        }
    }
}