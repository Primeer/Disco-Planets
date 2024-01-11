using Repository;
using UnityEngine;

namespace Service
{
    /// <summary>
    /// Позволяет применять к шарам физические эффекты
    /// </summary>
    public class BallPhysicService
    {
        private readonly BallsRepository repository;

        public BallPhysicService(BallsRepository repository)
        {
            this.repository = repository;
        }

        public void Explode(Vector2 position, float force, float radius = Mathf.Infinity)
        {
            float radius2 = radius * radius;
            
            foreach (var view in repository.Views)
            {
                Vector2 ballPosition = view.Position;
                Vector2 direction = ballPosition - position;
                float distance = direction.sqrMagnitude;

                if (distance <= radius2)
                {
                    view.AddForce(direction.normalized * force, ForceMode2D.Impulse);
                }
            }
        }
    }
}