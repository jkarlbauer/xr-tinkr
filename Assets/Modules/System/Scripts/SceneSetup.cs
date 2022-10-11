using UnityEngine;

namespace Xrtinkr.System
{
    public class SceneSetup : MonoBehaviour
    {

        [SerializeField]
        GameObject OVRCameraRig;

        [SerializeField]
        GameObject DesktopCameraRig;

        void Awake()
        {
            if (SystemState.isOculusQuest)
            {
                SetupQuestScene();
            }
            else if (SystemState.isDesktop)
            {
                SetupDesktopScene();
            }
        }

        private void SetupQuestScene()
        {
            OVRCameraRig.SetActive(true);
            DesktopCameraRig.SetActive(false);
        }

        private void SetupDesktopScene()
        {
            DesktopCameraRig.SetActive(true);
            OVRCameraRig.SetActive(false);
        }
    }
}


