using Boosters.Configs.Types;
using UnityEngine;

namespace Boosters.Configs
{
    [CreateAssetMenu(fileName = Name, menuName = "Scriptables/Boosters/Installer Config", order = 0)]
    public class BoostersInstallerConfig : ScriptableObject
    {
        public const string Name = "BoostersInstallerConfig";
        
        [SerializeField] private BoostersConfig boostersConfig;
        [SerializeField] private BombBoosterConfig bombConfig;
        [SerializeField] private MagnetBoosterConfig magnetConfig;
        [SerializeField] private QualityBoosterConfig qualityConfig;
        
        public BombBoosterConfig BombConfig => bombConfig;
        public BoostersConfig BoostersConfig => boostersConfig;
        public MagnetBoosterConfig MagnetConfig => magnetConfig;
        public QualityBoosterConfig QualityConfig => qualityConfig;
    }
}