using System;
using Model;
using Service.Features;
using Service.SaveLoad;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    public class GameService : IInitializable, IStartable, IDisposable
    {
        private const string CompleteSave = "game_completed";
        
        private readonly SaveLoadService saveLoadService;
        private readonly BallValueService ballValueService;
        private readonly FeatureOpenService featureOpenService;
        private readonly LevelModel levelModel;
        private readonly ScoreModel scoreModel;

        public GameService(SaveLoadService saveLoadService, BallValueService ballValueService, LevelModel levelModel, 
            ScoreModel scoreModel, FeatureOpenService featureOpenService)
        {
            this.saveLoadService = saveLoadService;
            this.ballValueService = ballValueService;
            this.levelModel = levelModel;
            this.scoreModel = scoreModel;
            this.featureOpenService = featureOpenService;
        }

        public void Initialize()
        {
            EventBus.MainBallMerged += OnMainBallMerged;
        }

        public void Start()
        {
            var isGameCompleted = PlayerPrefs.GetInt(CompleteSave, 0) == 1;

            if (isGameCompleted)
            {
                featureOpenService.OpenAllFeatures();
            }
        }

        public void Dispose()
        {
            EventBus.MainBallMerged -= OnMainBallMerged;
        }

        public void FreezeGame()
        {
            EventBus.GamePaused?.Invoke();
            Time.timeScale = 0;
        }

        public void UnfreezeGame()
        {
            EventBus.GameResumed?.Invoke();
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            saveLoadService.ClearSave();
            ballValueService.DeleteAllBalls();
            levelModel.ResetLevels();
            scoreModel.ResetScore();
        }

        private void OnMainBallMerged()
        {
            if (levelModel.IsLastLevel())
            {
                CompleteGame();
            }
        }

        private void CompleteGame()
        {
            PlayerPrefs.SetInt(CompleteSave, 1);
        }
    }
}