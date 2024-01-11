using System;
using System.Threading;
using Boosters.Configs;
using Model;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class BoosterPresenter : IInitializable, IDisposable
    {
        private readonly BoosterModel model;
        private readonly BoosterView view;

        private CancellationTokenSource[] cooldownTextCts = new CancellationTokenSource[2];

        public BoosterPresenter(BoosterModel model, BoosterView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            model.BoosterChanged += OnBoosterChanged;
            model.BoosterReady += OnBoosterReady;
            model.BoosterActivated += OnBoosterActivated;
            view.ButtonClicked += model.ActivateBooster;
        }

        public void Dispose()
        {
            model.BoosterChanged -= OnBoosterChanged;
            model.BoosterReady -= OnBoosterReady;
            model.BoosterActivated -= OnBoosterActivated;
            view.ButtonClicked -= model.ActivateBooster;

            for (var i = 0; i < cooldownTextCts.Length; i++)
            {
                cooldownTextCts[i]?.Cancel();
                cooldownTextCts[i]?.Dispose();
                cooldownTextCts[i] = null;
            }
        }

        private void OnBoosterChanged(int index, BoosterData data, float cooldown)
        {
            view.SetButtonSprite(index, data.sprite);
            view.SetButtonActive(index, false);
            view.SetBoosterActivated(index, false);

            cooldownTextCts[index]?.Cancel();
            cooldownTextCts[index] = new CancellationTokenSource();
            view.SetCooldownTextAsync(index, cooldown, cooldownTextCts[index].Token).Forget();
        }

        private void OnBoosterReady(int index)
        {
            view.SetButtonActive(index, true);
        }

        private void OnBoosterActivated(int index)
        {
            view.SetBoosterActivated(index, true);
        }
    }
}