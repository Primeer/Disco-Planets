using System;
using Model;
using Service;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class ScorePresenter : IInitializable, IStartable, IDisposable
    {
        private const string SaveKey = "bestScore";
        
        private readonly EffectFactory effectFactory;
        private readonly ScoreModel model;
        private readonly ScorePanelView view;
        private readonly LevelModel levelModel;
        private readonly Vector2 mainBallPosition;

        
        public ScorePresenter(ScorePanelView view, ScoreModel model, EffectFactory effectFactory, LevelModel levelModel,
            SceneContext sceneContext)
        {
            this.view = view;
            this.model = model;
            this.effectFactory = effectFactory;
            this.levelModel = levelModel;
            mainBallPosition = sceneContext.GravitationPoint;
        }

        public void Initialize()
        {
            EventBus.BallsMerged += OnBallsMerged;
            EventBus.MainBallMerged += OnMainBallMerged;
            model.ScoreChanged += view.AddDeltaScore;
            model.BestScoreChanged += OnBestScoreChanged;
        }

        public void Start()
        {
            int savedBestScore = PlayerPrefs.GetInt(SaveKey, 0);
            model.SetBestScore(savedBestScore);
        }

        public void Dispose()
        {
            EventBus.BallsMerged -= OnBallsMerged;
            EventBus.MainBallMerged -= OnMainBallMerged;
            model.ScoreChanged -= view.AddDeltaScore;
            model.BestScoreChanged -= OnBestScoreChanged;
        }

        private void OnBestScoreChanged(int score)
        {
            view.SetBestScore(score);
            PlayerPrefs.SetInt(SaveKey, score);
        }
        
        private void OnBallsMerged(int value, Vector2 position)
        {
            model.AddScore(value);
            effectFactory.CreateScoreEffect(value, position);
        }

        private void OnMainBallMerged()
        {
            OnBallsMerged(levelModel.GetMainBallScore(), mainBallPosition);
        }
    }
}
