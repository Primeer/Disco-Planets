using Boosters.Configs.Types;
using Model;
using Repository;
using VContainer;
using View;

namespace Boosters.Types
{
    /// <summary>
    /// Бустер "Качество"
    /// </summary>
    public class QualityBooster : AbstractBooster
    {
        [Inject] private readonly ThrowerModel throwerModel;
        [Inject] private readonly BoosterView  boosterView;
        [Inject] private readonly QualityBoosterConfig config;
        [Inject] private readonly ModifiersRepository modifiers;

        private int counter;


        protected override void OnActivate()
        {
            counter = config.Count;

            modifiers.ValueMod += config.Value;
            throwerModel.BallCreated += OnBallCreate;
            
            boosterView.SetCounterText(Index, config.Count, true);
        }

        private void OnBallCreate(BallView view)
        {
            counter--;
            
            boosterView.SetCounterText(Index, counter, true);

            if (counter == 0)
            {
                Deactivate();
            }
        }

        private void Deactivate()
        {
            boosterView.SetCounterText(Index, 0, false);
            
            modifiers.ValueMod -= config.Value;
            throwerModel.BallCreated -= OnBallCreate;

            Finish();
        }
    }
}