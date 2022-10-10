using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Xrtinkr.Debug
{
    public class LogConsoleToGUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        private Queue<string> _logQueue;

        private const int MAX_LOG_MESSAGES = 20;

        private void OnEnable()
        {
            _logQueue = new Queue<string>();
            Application.logMessageReceived += ProcessAndLogMessage;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= ProcessAndLogMessage;
        }

        private void ProcessAndLogMessage(string condition, string stackTrace, LogType type)
        {
            ClearLog();
            string message = $"[{type}] {condition} \n {stackTrace}";
            _logQueue.Enqueue(message);

            while (_logQueue.Count > MAX_LOG_MESSAGES){
                _logQueue.Dequeue();
            }

            foreach(string log in _logQueue)
            {
                Log(log);
            }
        }

        private void Log(string message) => _textMeshPro.text += message + "\n";
        private void ClearLog() => _textMeshPro.text = "";

    }
}

