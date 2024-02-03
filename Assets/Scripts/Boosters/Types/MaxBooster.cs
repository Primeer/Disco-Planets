using System.Linq;
using Model;
using Repository;
using Service;
using VContainer;

namespace Boosters.Types
{
    /// <summary>
    /// Бустер "Наибольший шар"
    /// </summary>
    public class MaxBooster : AbstractBooster
    {
        [Inject] private readonly ThrowerModel throwerModel;
        [Inject] private readonly BallFactory factory;
        [Inject] private readonly BallsRepository repository;

        protected override void OnActivate()
        {
            int value = repository.Values.Values.Max();
            var ball = factory.CreateThrowerBall(value);
            throwerModel.SetBall(ball);
            
            Finish();
        }
    }
}