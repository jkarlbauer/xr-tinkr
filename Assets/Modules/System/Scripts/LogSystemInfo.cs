using UnityEngine;

namespace Xrtinkr.System
{
    public class LogSystemInfo : MonoBehaviour
    {
        private void Awake()
        {
            LogSystemInfoText();
        }

        private void LogSystemInfoText()
        {
            Debug.Log(SystemState.GetSystemInfo());
            Debug.Log("is desktop: " + SystemState.isDesktop);
            Debug.Log("is quest: " + SystemState.isOculusQuest);
        }
    }
}

