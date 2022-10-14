using UnityEngine;
using Oculus.Interaction;
using UnityEngine.UI;
using System;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(RayInteractor))]
    [RequireComponent(typeof(LineRenderer))]
    public class TeleportInteraction : MonoBehaviour
    {
        [SerializeField]
        private GameObject _reticle;

        [SerializeField]
        private GameObject _OVRCameraRig;

        [SerializeField]
        private GameObject _visualDwellFeedback;

        private Image _dwellImage;

        private RayInteractor _rayInteractor;

        private LineRenderer _lineRenderer;

        private const float DWELL_TIME = 1.0f;
        private float _pinchStartTime;
        private bool _isPinching;

        private void OnEnable()
        {
            _rayInteractor = GetComponent<RayInteractor>();
            _rayInteractor.WhenStateChanged += ProcessState;
            SetupVisuals();
  
        }

        private void SetupVisuals()
        {
            _reticle = Instantiate(_reticle);
            _reticle.SetActive(false);
            _lineRenderer = GetComponent<LineRenderer>();
            _visualDwellFeedback = Instantiate(_visualDwellFeedback);
            _visualDwellFeedback.SetActive(false);
            _dwellImage = _visualDwellFeedback.GetComponentInChildren<Image>();

        }

        private void ProcessState(InteractorStateChangeArgs obj)
        {
            bool pinchStarted = HasPinchStarted(obj);
            bool pinchEnded = HasPinchEnded(obj);

            if (pinchStarted)
            {
                HandlePinchStarted();
            }

            if (pinchEnded)
            {
                HandlePinchEnded();
            }
        }

        private void HandlePinchStarted()
        {
            _isPinching = true;
            SetPinchStartTime();
            ShowReticle();
            ShowUI();
        }

        private void HandlePinchEnded()
        {
            _isPinching = false;
            HideReticle();
            HideUI();

            if (GetCurrentHoldTime() >= DWELL_TIME)
            {
                Teleport();
            }
        }

        private void Update()
        {
            UpdateVisuals();
        }
     

        private void UpdateVisuals()
        {
            UpdateLineRenderer();
            UpdateDwellVisuals();


            RaycastHit hit;
            if (Physics.Raycast(_rayInteractor.Ray.origin, _rayInteractor.Ray.direction, out hit, Mathf.Infinity, LayerMask.GetMask("TeleportTarget")))
            {
                _reticle.transform.position = hit.point;
            }
    

        }

        private void UpdateDwellVisuals()
        {
            _visualDwellFeedback.transform.position = _rayInteractor.Ray.origin;
            if (_isPinching)
            {
                _dwellImage.fillAmount = GetCurrentHoldTime()/DWELL_TIME;
            }

            _visualDwellFeedback.transform.forward = _visualDwellFeedback.transform.position - _OVRCameraRig.GetComponentInChildren<Camera>().transform.position;
        }

        private void UpdateLineRenderer()
        {
            _lineRenderer.SetPositions(new Vector3[] { _rayInteractor.Ray.origin, _rayInteractor.Ray.origin + _rayInteractor.Ray.direction });

        }

        private bool HasPinchStarted(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Hover && obj.NewState == InteractorState.Select;
        private bool HasPinchEnded(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Select && obj.NewState == InteractorState.Hover;
        private void ShowReticle() => _reticle.SetActive(true);
        private void HideReticle() => _reticle.SetActive(false);
        private void ShowUI()
        {
            _lineRenderer.enabled = true;
            _visualDwellFeedback.SetActive(true);
        }

        private void HideUI()
        {
            _lineRenderer.enabled = false;
            _visualDwellFeedback.SetActive(false);
        }

        private void Teleport() => _OVRCameraRig.transform.position = _reticle.transform.position;
        private void SetPinchStartTime() => _pinchStartTime = Time.time;
        private float GetCurrentHoldTime() => Time.time - _pinchStartTime;



    }

}
