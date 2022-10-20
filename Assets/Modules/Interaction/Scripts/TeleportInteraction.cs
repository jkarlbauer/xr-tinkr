using UnityEngine;
using Oculus.Interaction;
using Xrtinkr.Utils;
using System;

namespace Xrtinkr.Interaction
{
    public class TeleportInteraction : MonoBehaviour
    {

        [SerializeField]
        private IndexPinchSelector _pinchSelector;

        [SerializeField]
        private ISelector _selector;

        [SerializeField]
        private GameObject _teleportTarget;

        [SerializeField]
        private Transform _rayOrigin;

        [SerializeField]
        private float _dwellTime;

        private Timer _timer;

        public event Action PinchStarted;
        public event Action PinchEnded;

        private RaycastHit _currentRaycastHit;
        public RaycastHit CurrentRaycastHit { get => _currentRaycastHit; }

        private void OnEnable()
        {
            _pinchSelector.WhenSelected += HandlePinchStarted;
            _pinchSelector.WhenUnselected += HandlePinchEnded;

            _timer = new Timer();
            _currentRaycastHit = new RaycastHit();

        }

        private void HandlePinchStarted()
        {
            _timer.StartTimer();
            PinchStarted?.Invoke();
        }

        protected void HandlePinchEnded()
        {
            PinchEnded?.Invoke();

            if(GetCurrentPinchingDuration() >= _dwellTime)
            {
                Teleport();
            }

            _timer.ResetTimer();
        }

        private void Update()
        {
            Vector3 _orign = _rayOrigin.transform.position;
            Quaternion _rotation = _rayOrigin.transform.rotation;
            Vector3 _forward = _rotation * Vector3.forward;

            RaycastHit hit;

            if (Physics.Raycast(
                _orign,
                _forward,
                out hit,
                Mathf.Infinity,
                LayerMask.GetMask("TeleportTarget")))
            {
                _currentRaycastHit = hit;
            }
        }


        private void Teleport() => _teleportTarget.transform.position = CurrentRaycastHit.point;

        public Vector3 GetRayOrigin() => _rayOrigin.position;

        public float GetCurrentPinchingDuration() => _timer.ElapsedTimeSinceStart;



    }

}
