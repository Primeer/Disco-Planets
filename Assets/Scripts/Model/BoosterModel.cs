using System;
using System.Collections.Generic;
using System.Linq;
using Boosters;
using Boosters.Types;
using UnityEngine;
using VContainer;

namespace Model
{
    /// <summary>
    /// Модель управления бустерами. Спавнит бустеры рандомно в 2 слота.
    /// </summary>
    public class BoosterModel : IDisposable
    {
        private const string SaveKey = "boosters_enabled";
        private readonly Dictionary<BoosterType, AbstractBooster> boosters;

        public bool IsBoostersEnabled;
        public Action<BoosterType> BoosterReset;
        public Action<BoosterType> BoosterReady;

        public BoosterModel(IObjectResolver container)
        {
            boosters = new Dictionary<BoosterType, AbstractBooster>
            {
                { BoosterType.Max, new MaxBooster() },
                { BoosterType.Bomb, new BombBooster() },
                { BoosterType.Quality, new QualityBooster() },
                { BoosterType.Magnet, new MagnetBooster() },
            };
            
            foreach (var booster in boosters.Values)
            {
                container.Inject(booster);
            }
        }

        public void InitializeBoosters()
        {
            var boostersTypes = boosters.Keys.ToArray();

            for (int i = 0; i < boostersTypes.Length; i++)
            {
                var type = boostersTypes[i];
                var booster = boosters[type];
                
                booster.Ready += OnBoosterReady;
                booster.Finished += OnBoosterFinished;
                
                booster.Initialize(type, i);
                ResetBooster(type);
            }
            
            Load();
            
            if (IsBoostersEnabled)
            {
                EnableAllBoosters();
            }
        }

        public void ResetAllBoosters()
        {
            IsBoostersEnabled = false;
            boosters.Keys.ToList().ForEach(ResetBooster);
        }

        private void ResetBooster(BoosterType type)
        {
            boosters[type].Reset();
            BoosterReset?.Invoke(type);
        }

        public void EnableAllBoosters()
        {
            IsBoostersEnabled = true;
            boosters.Keys.ToList().ForEach(EnableBooster);
        }

        public void EnableBooster(BoosterType type) => boosters[type].Enable();

        public void ActivateBooster(BoosterType type) => boosters[type].Activate();
        
        public BoosterState GetBoosterState(BoosterType type) => boosters[type].State;

        private void OnBoosterReady(BoosterType type) => BoosterReady?.Invoke(type);

        private void OnBoosterFinished(BoosterType _) => ResetAllBoosters();

        public void Load() => IsBoostersEnabled = PlayerPrefs.GetInt(SaveKey, 0) == 1;
        
        public void Save() => PlayerPrefs.SetInt(SaveKey, IsBoostersEnabled ? 1 : 0);

        public void Dispose()
        {
            Save();
            
            foreach (var booster in boosters.Values)
            {
                booster.Dispose();
            }
            
            boosters.Clear();
        }
    }
}