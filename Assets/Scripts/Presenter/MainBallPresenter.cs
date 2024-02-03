using System;
using Configs.Levels;
using Service;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class MainBallPresenter : IInitializable, IDisposable
    {
        private readonly BallValueService valueService;
        private readonly LevelConfig levelConfig;
        private readonly BallView view;

        public MainBallPresenter(BallValueService valueService, SceneContext sceneContext, LevelConfig levelConfig)
        {
            this.valueService = valueService;
            this.levelConfig = levelConfig;
            view = sceneContext.MainBall;
        }

        public void Initialize()
        {
            EventBus.LevelChanged += OnLevelChanged;
        }

        public void Dispose()
        {
            EventBus.LevelChanged -= OnLevelChanged;
        }

        private void OnLevelChanged(int level)
        {
            int value = levelConfig.GetLevelData(level).MaxValue;
            valueService.SetBallValue(view, value);
            view.gameObject.SetActive(true);
        }
    }
}