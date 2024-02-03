using Configs;
using Repository;

namespace Service
{
    /// <summary>
    /// Позволяет воспроизводить вибрацию
    /// </summary>
    public class VibrationService
    {
        private readonly VibrationWrapper vibrationWrapper;
        private readonly SettingsRepository settings;
        private readonly int vibrationDuration;
        private readonly int vibrationAmplitude;

        public VibrationService(VibrationWrapper vibrationWrapper, CommonConfig commonConfig, SettingsRepository settings)
        {
            this.vibrationWrapper = vibrationWrapper;
            this.settings = settings;
            vibrationDuration = commonConfig.VibrationDurationMs;
            vibrationAmplitude = commonConfig.VibrationAmplitude;
        }

        public void PlayVibration()
        {
            if (settings.IsVibrationEnabled)
            {
                vibrationWrapper.VibrateAndroid(vibrationDuration, vibrationAmplitude);
            }
        }
    }
}