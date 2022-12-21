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

        public event Action InteractionStarted;
        public event Action InteractionEnded;
        public event Action InteractionWentToInvalid;
        public event Action InteractionWentToValid;

        private bool _interactionIsValid;

        private RaycastHit _currentRaycastHit;
        public RaycastHit CurrentRaycastHit { get => _currentRaycastHit; }
        public bool InteractionIsValid { get => _interactionIsValid;}

        private void OnEnable()
        {
            _pinchSelector.WhenSelected += HandlePinchStarted;
            _pinchSelector.WhenUnselected += HandlePinchEnded;
            InteractionWentToInvalid += HandleInterrupt;

            _timer = new Timer();
            _currentRaycastHit = new RaycastHit();
        }

        private void HandleInterrupt()
        {
            InteractionEnded.Invoke();
            ResetTimer();
        }

        private void HandlePinchStarted()
        {
            if (!_interactionIsValid)
            {
                return;
            }

            _timer.StartTimer();
            InteractionStarted?.Invoke();
        }

        protected void HandlePinchEnded()
        {

            if (!_interactionIsValid)
            {
                return;
            }

            InteractionEnded?.Invoke();

            if(GetCurrentPinchingDuration() >= _dwellTime)
            {
                Teleport();
            }

            ResetTimer();
        }

        private void Update()
        {
            Vector3 origin = _rayOrigin.transform.position;
            Quaternion rotation = _rayOrigin.transform.rotation;
            Vector3 forward = rotation * Vector3.forward;

            RaycastHit hit;

            if (Physics.Raycast(
                origin,
                forward,
                out hit,
                Mathf.Infinity,
                LayerMask.GetMask("TeleportTarget")))
            {
                _currentRaycastHit = hit;

                if (!_interactionIsValid)
                {
                    InteractionWentToValid.Invoke();
                }

                _interactionIsValid = true;
            } else {

                if (_interactionIsValid)
                {
                    InteractionWentToInvalid.Invoke();
                }

                _interactionIsValid = false;
            }
        }


        private void Teleport()
        {
            if (InteractionIsValid)
            {
                _teleportTarget.transform.position = CurrentRaycastHit.point;
            }

            DisableController();
        } 

        public Vector3 GetRayOrigin() => _rayOrigin.position;

        public float GetCurrentPinchingDuration() => _timer.ElapsedTimeSinceStart;

        private void ResetTimer() => _timer.ResetTimer();

        private void DisableController() => gameObject.SetActive(false);



    }

}
