using UnityEngine;
using UnityEngine.Rendering;

public class RemoveSplash
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Init()
    {
#if UNITY_WEBGL
        Application.focusChanged += Application_focusChanged;
# else
        System.Threading.Tasks.Task.Run(() => { SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate); });
#endif

    }

#if UNITY_WEBGL
    private static void Application_focusChanged(bool obj)
    {
        Application.focusChanged -= Application_focusChanged;
        SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
    }
#endif
}