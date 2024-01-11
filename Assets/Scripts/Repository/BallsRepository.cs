using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Repository
{
    /// <summary>
    /// Хранит списки шаров и их значений
    /// </summary>
    public class BallsRepository : IInitializable, IDisposable
    {
        public List<BallView> Views = new(20);
        public Dictionary<BallView, int> Values = new(20);

        public Action<List<BallView>> BallsChanged;
        
        public void Initialize()
        {
            EventBus.BallCreated += OnBallCreated;
            EventBus.BallDestroyed += OnBallDestroyed;
        }

        public void Dispose()
        {
            EventBus.BallCreated -= OnBallCreated;
            EventBus.BallDestroyed -= OnBallDestroyed;
        }

        private void OnBallCreated(BallView view, int value)
        {
            Views.Add(view);
            Values[view] = value;
            
            BallsChanged?.Invoke(Views);
        }

        private void OnBallDestroyed(BallView view)
        {
            Views.Remove(view);
            Values.Remove(view);
            
            BallsChanged?.Invoke(Views);
        }
        
        public List<BallView> GetBallsInArea(Vector2 center, float radius)
        {
            float radiusSqr = radius * radius;

            var result = 
                from t in Views 
                where (t.Position - center).sqrMagnitude <= radiusSqr 
                select t;
            
            return result.ToList();
        }
    }
}