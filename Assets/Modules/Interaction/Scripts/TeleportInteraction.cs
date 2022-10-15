using UnityEngine;
using Oculus.Interaction;
using Xrtinkr.Utils;
using Xrtinkr.UI;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(LineRenderer))]
    public class TeleportInteraction : BaseInteraction
    {
        [SerializeField]
        private GameObject _reticle;

        [SerializeField]
        private GameObject _OVRCameraRig;

        [SerializeField]
        private GameObject _dwellTimerPrefab;

        private DwellTimer _dwellTimer;

        [SerializeField]
        private float _dwellTime;

        private RayInteractor _rayInteractor;

        private LineRenderer _lineRenderer;


        private bool _isPinching;

        private Timer _timer;

        private void OnEnable()
        {
            RegisterInteractor();

            _rayInteractor = interactor as RayInteractor;

            SetupDwellTimer();
            SetupReticle();
            SetupLineRenderer();

            _timer = new Timer();

        }

        private void SetupLineRenderer()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void SetupReticle()
        {
            _reticle = Instantiate(_reticle);
            _reticle.SetActive(false);
        }

        private void SetupDwellTimer()
        {
            _dwellTimerPrefab = Instantiate(_dwellTimerPrefab);
            _dwellTimer = _dwellTimerPrefab.GetComponent<DwellTimer>();
            _dwellTimer.Initialize();
            _dwellTimer.DwellTime = _dwellTime;
            _dwellTimer.SetOrientationTarget(_OVRCameraRig.GetComponentInChildren<Camera>().transform);
        }

        protected override void HandlePinchStarted()
        {
            _isPinching = true;
            _timer.StartTimer();
            ShowReticle();
            ShowUI();
        }

        protected override void HandlePinchEnded()
        {
            _isPinching = false;
            HideReticle();
            HideUI();

            if (_timer.ElapsedTimeSinceStart >= _dwellTime)
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
            UpdateReticle();


        }

        private void UpdateReticle()
        {
            RaycastHit hit;
            if (Physics.Raycast(_rayInteractor.Ray.origin, _rayInteractor.Ray.direction, out hit, Mathf.Infinity, LayerMask.GetMask("TeleportTarget")))
            {
                _reticle.transform.position = hit.point;
            }
        }

        private void UpdateDwellVisuals()
        {

            _dwellTimer.UpdatePosition(_rayInteractor.Ray.origin);
            if (_isPinching)
            {
                _dwellTimer.SetDwellFill(_timer.ElapsedTimeSinceStart / _dwellTime);
            }
        }

        private void UpdateLineRenderer()
        {
            _lineRenderer.SetPositions(new Vector3[] { _rayInteractor.Ray.origin, _rayInteractor.Ray.origin + _rayInteractor.Ray.direction });
        }

        private void ShowReticle() => _reticle.SetActive(true);
        private void HideReticle() => _reticle.SetActive(false);
        private void ShowUI()
        {
            _lineRenderer.enabled = true;
            _dwellTimer.Show();
        }

        private void HideUI()
        {
            _lineRenderer.enabled = false;
            _dwellTimer.Hide();
        }

        private void Teleport() => _OVRCameraRig.transform.position = _reticle.transform.position;


    }

}
