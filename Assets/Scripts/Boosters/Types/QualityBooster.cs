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

            // modifiers.ValueMod += config.Value;
            modifiers.FixValue += config.Value;
            modifiers.IsFixValue = true;
            throwerModel.BallCreated += OnBallCreate;
            
            boosterView.SetButtonText(Index, config.Count.ToString());
            
            throwerModel.SpawnBall();
        }

        private void OnBallCreate(BallView view)
        {
            counter--;
            
            boosterView.SetButtonText(Index, counter.ToString());

            if (counter == 0)
            {
                OnDeactivate();
            }
        }

        protected override void OnDeactivate()
        {
            boosterView.SetButtonText(Index, "");
            
            // modifiers.ValueMod -= config.Value;
            modifiers.IsFixValue = false;
            modifiers.FixValue -= config.Value;
            throwerModel.BallCreated -= OnBallCreate;

            Finish();
        }
    }
}