using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Xrtinkr.Debug
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LogConsoleToGUI : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private LogMessageProcessor _logMessageProcessor;
        
        private void OnEnable()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _logMessageProcessor = new LogMessageProcessor();
            Application.logMessageReceived += ProcessAndLogMessage;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= ProcessAndLogMessage;
        }

        private void ProcessAndLogMessage(string condition, string stackTrace, LogType type)
        {

            ClearTextField();
            _logMessageProcessor.ProcessLogMessage(condition, stackTrace, type);

            foreach (string log in _logMessageProcessor.LogQueue)
            {
                PrintToConsole(log);
            }
        }

        private void PrintToConsole(string message) => _textMeshPro.text += message + "\n";
        private void ClearTextField() => _textMeshPro.text = "";

    }

    public class LogMessageProcessor
    {
        private Queue<string> _logQueue;
        public Queue<string> LogQueue { 
            get => _logQueue;
        }

        private const int MAX_ALLOWED_LOG_MESSAGES = 10;

        public LogMessageProcessor()
        {
            _logQueue = new Queue<string>();
        }

        public void ProcessLogMessage(string condition, string stackTrace, LogType type)
        {
            string message = ParseMessage(condition, stackTrace, type);
            EnqueueMessage(message);
            TrimLogQueueToMaxAllowedMessages();
        }

        private string ParseMessage(string condition, string stackTrace, LogType type)
        {
            return $"[{type}] {condition} \n {stackTrace}";
        }

        private void EnqueueMessage(string message)
        {
            _logQueue.Enqueue(message);
        }

        private void TrimLogQueueToMaxAllowedMessages()
        {
            while (_logQueue.Count > MAX_ALLOWED_LOG_MESSAGES)
            {
                _logQueue.Dequeue();
            }
        }
    }



}

