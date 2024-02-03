using System;
using System.Collections.Generic;
using Boosters;
using Boosters.Configs;
using Model;
using Service;
using Service.Features;
using UnityEngine;
using VContainer.Unity;
using View;

namespace Presenter
{
    public class BoosterPresenter : IInitializable, IStartable, IDisposable
    {
        private readonly BoosterModel model;
        private readonly BoosterView view;
        private readonly TutorialService tutorialService;
        private readonly VibrationService vibrationService;
        private readonly Dictionary<BoosterType, BoosterData> boosterData;
        private readonly int boostersCount;
        
        public BoosterPresenter(BoosterModel model, BoosterView view, BoostersConfig config, 
            TutorialService tutorialService, VibrationService vibrationService)
        {
            this.model = model;
            this.view = view;
            this.tutorialService = tutorialService;
            this.vibrationService = vibrationService;
            boosterData = config.Boosters;
            boostersCount = config.Boosters.Count;
        }

        public void Initialize()
        {
            model.BoosterReset += OnBoosterReset;
            model.BoosterReady += OnBoosterReady;
            view.ButtonClicked += OnBoosterClicked;
            EventBus.LevelChanged += OnLevelChanged;
            EventBus.FeatureOpened += OnFeatureOpened;
        }

        public void Start()
        {
            model.InitializeBoosters();
        }

        public void Dispose()
        {
            model.BoosterReset -= OnBoosterReset;
            model.BoosterReady -= OnBoosterReady;
            view.ButtonClicked -= OnBoosterClicked;
            EventBus.LevelChanged -= OnLevelChanged;
            EventBus.FeatureOpened -= OnFeatureOpened;
            
            model.Dispose();
        }

        private void OnLevelChanged(int _)
        {
            model.ResetAllBoosters();
            model.EnableAllBoosters();
        }

        private void OnFeatureOpened(FeatureEnum feature)
        {
            var type = GetBoosterByFeature(feature);

            if (type is { } boosterType)
            {
                view.ShowBooster((int)boosterType, true);
                tutorialService.ShowBoosterTutorial(boosterData[boosterType].tutorialWindow);
            }
        }

        private void OnBoosterReset(BoosterType type)
        {
            int index = (int)type;
            
            view.SetButtonSprite(index, boosterData[type].sprite);
            UnlockButtons();
            view.SetBoosterActivated(index, false);
            view.SetAd(index, true);
        }

        private void OnBoosterReady(BoosterType type)
        {
            view.SetAd((int)type, false);
        }

        private void OnBoosterClicked(int index)
        {
            vibrationService.PlayVibration();
            
            var type = (BoosterType)index;
            var state = model.GetBoosterState(type);
            
            switch (state)
            {
                case BoosterState.Disabled:
                    Debug.Log("ShowAd");
                    model.EnableAllBoosters();
                    break;
                case BoosterState.Ready:
                    Debug.Log($"Activate booster: {type.ToString()}");
                    LockButtons(index);
                    ActivateBooster(type);
                    break;
            }
        }
        
        private void ActivateBooster(BoosterType type)
        {
            view.SetBoosterActivated((int)type, true);
            model.ActivateBooster(type);
        }

        private void LockButtons(int exIndex)
        {
            for (int i = 0; i < boostersCount; i++)
            {
                if (i == exIndex)
                {
                    continue;
                }
                
                view.SetButtonEnabled(i, false);
            }
        }
        
        private void UnlockButtons()
        {
            for (int i = 0; i < boostersCount; i++)
            {
                view.SetButtonEnabled(i, true);
                
                if (model.GetBoosterState((BoosterType)i) == BoosterState.Disabled)
                {
                    view.SetAd(i, true);
                }
            }
        }

        private BoosterType? GetBoosterByFeature(FeatureEnum feature)
        {
            return feature switch
            {
                FeatureEnum.BoosterMax => BoosterType.Max,
                FeatureEnum.BoosterBomb => BoosterType.Bomb,
                FeatureEnum.BoosterQuality => BoosterType.Quality,
                FeatureEnum.BoosterMagnet => BoosterType.Magnet,
                _ => null
            };
        }
    }
}