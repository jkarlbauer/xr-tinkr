
using UnityEngine;

namespace Xrtinkr.Utils
{
    public class Timer
    {
        private float _startTime = -1f;

        public float ElapsedTimeSinceStart {
            get => GetElapsedTimeSinceStart();
        }

        public void StartTimer() => _startTime = Time.time;

        private float GetElapsedTimeSinceStart()
        {
            if (IsTimerValid())
            {
                return Time.time - _startTime;
            }
            else
            {
                return -1f;
            }
        }

        private bool IsTimerValid() => _startTime > 0;

    }



}
