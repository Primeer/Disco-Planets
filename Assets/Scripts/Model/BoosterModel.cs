using System;
using System.Collections.Generic;
using System.Linq;
using Boosters;
using Boosters.Configs;
using Boosters.Types;
using VContainer;
using Random = UnityEngine.Random;

namespace Model
{
    /// <summary>
    /// Модель управления бустерами. Спавнит бустеры рандомно в 2 слота.
    /// </summary>
    public class BoosterModel
    {
        private readonly Dictionary<BoosterType, BoosterData> boosterData;
        private readonly Dictionary<BoosterType, AbstractBooster> boosters; 
        private readonly BoosterType[] activeBoosters = new BoosterType[2];
        private readonly float cooldown;
        
        public Action<int, BoosterData, float> BoosterChanged;
        public Action<int> BoosterReady;
        public Action<int> BoosterActivated;
        
        
        public BoosterModel(BoostersConfig config, IObjectResolver container)
        {
            cooldown = config.Cooldown;
            boosterData = config.Boosters;
            
            boosters = new Dictionary<BoosterType, AbstractBooster>
            {
                { BoosterType.Bomb, new BombBooster() },
                { BoosterType.Magnet, new MagnetBooster() },
                { BoosterType.Max, new MaxBooster() },
                { BoosterType.Quality, new QualityBooster() }
            };
            
            foreach (var booster in boosters.Values)
            {
                container.Inject(booster);
            }
        }

        public void RefreshBooster(int index)
        {
            var existBoosters = activeBoosters.ToList();
            BoosterType type = GetRandomBoosterType(existBoosters);
            activeBoosters[index] = type;

            var booster = boosters[type];
            booster.Initialize(cooldown, index);
            booster.Ready += () => BoosterReady?.Invoke(index);
            booster.Finished += () => OnBoosterFinish(index);
            
            BoosterChanged?.Invoke(index, boosterData[type], cooldown);
        }

        public void ActivateBooster(int index)
        {
            var type = activeBoosters[index];
            BoosterActivated?.Invoke(index);
            boosters[type].Activate();
        }

        private void OnBoosterFinish(int index)
        {
            RefreshBooster(index);
        }

        private BoosterType GetRandomBoosterType(List<BoosterType> excludeTypes)
        {
            while (true)
            {
                int index = Random.Range(0, boosterData.Count);
                BoosterType type = boosterData.Keys.ToArray()[index];

                if (!excludeTypes.Contains(type))
                {
                    return type;
                }
            }
        }
    }
}