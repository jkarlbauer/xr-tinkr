using UnityEngine;
using Oculus.Interaction;

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

        private RayInteractor _rayInteractor;

        private LineRenderer _lineRenderer;

        private const float DWELL_TIME = 1.5f;
        private float _pinchStartTime;

        private void OnEnable()
        {
            _rayInteractor = GetComponent<RayInteractor>();
            _rayInteractor.WhenStateChanged += ProcessState;
            _reticle = Instantiate(_reticle);
            _reticle.SetActive(false);
            _lineRenderer = GetComponent<LineRenderer>();

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
            SetPinchStartTime();
            ShowReticle();
            ShowPointerRay();
        }

        private void HandlePinchEnded()
        {
            HideReticle();
            HidePointerRay();

            if (GetCurrentHoldTime() >= DWELL_TIME)
            {
                Teleport();
            }
        }

        private void Update()
        {
            _lineRenderer.SetPositions(new Vector3[] { _rayInteractor.Ray.origin, _rayInteractor.Ray.origin + _rayInteractor.Ray.direction });

            RaycastHit hit;
            if (Physics.Raycast(_rayInteractor.Ray.origin, _rayInteractor.Ray.direction, out hit, Mathf.Infinity, LayerMask.GetMask("TeleportTarget")))
            {
                _reticle.transform.position = hit.point;
            }
        }

        private bool HasPinchStarted(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Hover && obj.NewState == InteractorState.Select;
        private bool HasPinchEnded(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Select && obj.NewState == InteractorState.Hover;
        private void ShowReticle() => _reticle.SetActive(true);
        private void HideReticle() => _reticle.SetActive(false);
        private void ShowPointerRay() => _lineRenderer.enabled = true;
        private void HidePointerRay() => _lineRenderer.enabled = false;
        private void Teleport() => _OVRCameraRig.transform.position = _reticle.transform.position;
        private void SetPinchStartTime() => _pinchStartTime = Time.time;
        private float GetCurrentHoldTime() => Time.time - _pinchStartTime;



    }

}
