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

        private LineRenderer lineRenderer;

        private void OnEnable()
        {
            _rayInteractor = GetComponent<RayInteractor>();
            _rayInteractor.WhenStateChanged += ProcessState;
            _reticle = Instantiate(_reticle);
            _reticle.SetActive(false);
            lineRenderer = GetComponent<LineRenderer>();

        }

        private void ProcessState(InteractorStateChangeArgs obj)
        {

            Debug.Log(obj.PreviousState);
            Debug.Log(obj.NewState);
            if (obj.PreviousState == InteractorState.Hover && obj.NewState == InteractorState.Select)
            {
                ShowReticle();
            }

            if (obj.PreviousState == InteractorState.Select && obj.NewState == InteractorState.Hover)
            {
                HideReticle();
                Teleport();
            }
        }

        private void Update()
        {
            lineRenderer.SetPositions(new Vector3[] { _rayInteractor.Ray.origin, _rayInteractor.Ray.origin + _rayInteractor.Ray.direction });

            RaycastHit hit;
            if (Physics.Raycast(_rayInteractor.Ray.origin, _rayInteractor.Ray.direction, out hit, Mathf.Infinity, LayerMask.GetMask("TeleportTarget")))
            {
                _reticle.transform.position = hit.point;
            }


        }

        private void ShowReticle() => _reticle.SetActive(true);
        private void HideReticle() => _reticle.SetActive(false);
        private void Teleport() => _OVRCameraRig.transform.position = _reticle.transform.position;

    }

}
