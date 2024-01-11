using Boosters;
using Configs;
using Configs.Levels;
using Input;
using Localization;
using Model;
using Presenter;
using Repository;
using Service;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using VContainer;
using VContainer.Unity;
using View;

public class VStartup : LifetimeScope
{
    [Header("Views")]
    [SerializeField] private GameInputView gameInputView;
    [SerializeField] private LevelView levelView;
    [SerializeField] private ScorePanelView scorePanelView;
    [SerializeField] private ThrowerView throwerView;
    [SerializeField] private BoosterView boosterView;
    [SerializeField] private BallsLimitView ballsLimitView;
    [SerializeField] private TimerView timerView;
    [SerializeField] private VibrationButtonView vibrationButtonView;
    [SerializeField] private DebugButtonsView debugButtonsView;
        
    [Space]
    [SerializeField] private SceneContext sceneContext;
    [SerializeField] private InputSystemUIInputModule uiInputModule;
        
    [Header("Windows")]
    [SerializeField] private WinWindow winWindow;
        
    [Header("Configs")]
    [SerializeField] private LocalizationData localizationData;
    [SerializeField] private CommonConfig commonConfig;
    [SerializeField] private LevelConfig levelConfig;
    [SerializeField] private ChangeLevelEffectsConfig levelEffectsConfig;
    [SerializeField] private BallFlightConfig ballFlightConfig;
    [SerializeField] private BallValuesConfig ballValuesConfig;
        
    [Header("Prefabs")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject mergeEffectPrefab;
    [SerializeField] private GameObject scoreEffectPrefab;


    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(gameInputView);
        builder.RegisterComponent(levelView);
        builder.RegisterComponent(scorePanelView);
        builder.RegisterComponent(throwerView);
        builder.RegisterComponent(boosterView);
        builder.RegisterComponent(ballsLimitView);
        builder.RegisterComponent(timerView);
        builder.RegisterComponent(vibrationButtonView);
        builder.RegisterComponent(debugButtonsView);
            
        builder.RegisterComponent(sceneContext);
        builder.RegisterComponent(uiInputModule);

        builder.RegisterComponent(winWindow);

        builder.RegisterInstance(localizationData);
        builder.RegisterInstance(commonConfig);
        builder.RegisterInstance(levelConfig);
        builder.RegisterInstance(levelEffectsConfig);
        builder.RegisterInstance(ballFlightConfig);
        builder.RegisterInstance(ballValuesConfig);

        builder.RegisterEntryPoint<GameInputProvider>().AsSelf();
        builder.RegisterEntryPoint<LocalizationService>().AsSelf();
            
        builder.RegisterEntryPoint<BallsRepository>().AsSelf();
        builder.Register<ModifiersRepository>(Lifetime.Singleton);
        builder.Register<SettingsRepository>(Lifetime.Singleton);
            
        builder.Register<EffectFactory>(Lifetime.Singleton)
            .WithParameter("mergeEffectPrefab", mergeEffectPrefab)
            .WithParameter("scoreEffectPrefab", scoreEffectPrefab);
                
        builder.Register<BallPhysicService>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BallValueService>().AsSelf();
            
        builder.Register<BallFactory>(Lifetime.Singleton)
            .WithParameter("ballPrefab", ballPrefab);
                
        builder.RegisterEntryPoint<BallMergeService>().AsSelf();

        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScorePresenter>().AsSelf();

        builder.Register<ThrowerModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ThrowerPresenter>().AsSelf();
            
        builder.Register<BallModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BallPresenter>().AsSelf();

        builder.Register<LevelModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<LevelPresenter>().AsSelf();

        builder.RegisterEntryPoint<MainBallPresenter>().AsSelf();

        BoostersInstaller.Install(builder);

        builder.Register<ScreenDispatcher>(Lifetime.Singleton);
        builder.Register<ChangeLevelModel>(Lifetime.Singleton);
            
        builder.Register<BallsLimitModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BallsLimitPresenter>().AsSelf();

        builder.RegisterEntryPoint<VibrationButtonPresenter>().AsSelf();
        builder.RegisterEntryPoint<VibrationWrapper>().AsSelf();
        builder.Register<VibrationService>(Lifetime.Singleton);

        builder.RegisterEntryPoint<DebugButtonsPresenter>();
            
        builder.RegisterEntryPoint<GameInitializer>();
            
        builder.RegisterEntryPointExceptionHandler(Debug.LogException);
    }
}