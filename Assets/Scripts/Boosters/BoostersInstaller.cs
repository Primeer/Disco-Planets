using Boosters.Configs;
using Model;
using Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Boosters
{
    public class BoostersInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            var config = Resources.Load<BoostersInstallerConfig>(BoostersInstallerConfig.Name);

            builder.RegisterInstance(config.BoostersConfig);
            builder.RegisterInstance(config.BombConfig);
            builder.RegisterInstance(config.MagnetConfig);
            builder.RegisterInstance(config.QualityConfig);
            
            builder.Register<BoosterModel>(Lifetime.Scoped);
            builder.RegisterEntryPoint<BoosterPresenter>();
        }
    }
}