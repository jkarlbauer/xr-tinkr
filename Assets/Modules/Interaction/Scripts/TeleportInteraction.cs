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
        public event Action InputWentToInvalid;
        public event Action InputWentToValid;


        private bool _interactionIsValid;

        private RaycastHit _currentRaycastHit;
        public RaycastHit CurrentRaycastHit { get => _currentRaycastHit; }
        public bool InteractionIsValid { get => _interactionIsValid;}

        private void OnEnable()
        {
            _pinchSelector.WhenSelected += HandlePinchStarted;
            _pinchSelector.WhenUnselected += HandlePinchEnded;

            _timer = new Timer();
            _currentRaycastHit = new RaycastHit();

        }

        private void HandlePinchStarted()
        {
            if (!_interactionIsValid)
            {
                return;
            }

            _timer.StartTimer();
            PinchStarted?.Invoke();
        }

        protected void HandlePinchEnded()
        {

            if (!_interactionIsValid)
            {
                return;
            }

            PinchEnded?.Invoke();

            if(GetCurrentPinchingDuration() >= _dwellTime)
            {
                Teleport();
            }

            _timer.ResetTimer();
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
                    InputWentToValid.Invoke();
                }

                _interactionIsValid = true;
            } else {

                if (_interactionIsValid)
                {
                    InputWentToInvalid.Invoke();
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
        } 

        public Vector3 GetRayOrigin() => _rayOrigin.position;

        public float GetCurrentPinchingDuration() => _timer.ElapsedTimeSinceStart;



    }

}
