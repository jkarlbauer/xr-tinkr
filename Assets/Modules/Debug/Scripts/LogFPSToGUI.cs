
using System.Linq;
using TMPro;
using UnityEngine;

namespace Xrtinkr.Debug
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LogFPSToGUI : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        private float[] fpsQueue;
        private const int SMOOTHING_CONSTANT = 150;
        private int queueIndex = 0;

        private void OnEnable()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            fpsQueue = new float[SMOOTHING_CONSTANT];
        }

        private void Update()
        {
            float current_fps = (int)(1f / Time.unscaledDeltaTime);

            queueIndex++;
            queueIndex %= SMOOTHING_CONSTANT;
            fpsQueue[queueIndex] = current_fps;
            int average_fps = (int) fpsQueue.Average();
            textMeshProUGUI.text = average_fps.ToString();
        }
    }

}
