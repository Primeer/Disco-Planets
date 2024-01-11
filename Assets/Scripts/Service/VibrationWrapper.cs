using System;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    /// <summary>
    /// Обеспечивает возможность воспроизвести вибрацию на android-устройствах
    /// </summary>
    public class VibrationWrapper : IInitializable
    {
        private AndroidJavaClass unityPlayer;
        private AndroidJavaObject currentActivity;
        private AndroidJavaObject vibrator;
        private AndroidJavaObject context;
        private AndroidJavaClass vibrationEffect;
        private bool isHasVibration = true;
        
        public void Initialize()
        {
            if (Application.isMobilePlatform)
            {
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

                if (AndroidVersion >= 26)
                {
                    vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
                }
            }

            isHasVibration = HasVibrator();
        }
        
        public void VibrateAndroid(long milliseconds, int amplitude = -1)
        {
            if (Application.isMobilePlatform && isHasVibration)
            {
                if (AndroidVersion >= 26)
                {
                    AndroidJavaObject createOneShot = vibrationEffect.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, amplitude);
                    vibrator.Call("vibrate", createOneShot);
                }
                else
                {
                    vibrator.Call("vibrate", milliseconds);
                }
            }
        }

        private bool HasVibrator()
        {
            if (Application.isMobilePlatform)
            {
                AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
                string contextVibratorService = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
                AndroidJavaObject systemService = context.Call<AndroidJavaObject>("getSystemService", contextVibratorService);
                
                if (systemService.Call<bool>("hasVibrator"))
                {
                    return true;
                }
            }

            return false;
        }

        private int AndroidVersion
        {
            get
            {
                int iVersionNumber = 0;
                
                if (Application.platform == RuntimePlatform.Android)
                {
                    string androidVersion = SystemInfo.operatingSystem;
                    int sdkPos = androidVersion.IndexOf("API-", StringComparison.Ordinal);
                    iVersionNumber = int.Parse(androidVersion.Substring(sdkPos + 4, 2));
                }
                
                return iVersionNumber;
            }
        }
    }
}