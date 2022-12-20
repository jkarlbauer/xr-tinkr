
using UnityEngine;
using Xrtinkr.Interaction;

namespace Xrtinkr.UI.Teleport
{
    [RequireComponent(typeof(LineRenderer))]
    public class TeleportRayVisuals : MonoBehaviour
    {

        [SerializeField]
        private TeleportInteraction _teleportInteraction;

        [SerializeField]
        private float _lineRendererLength;

        [SerializeField]
        private Material _activeMaterial;

        [SerializeField]
        private Material _inactiveMaterial;

        private LineRenderer _lineRenderer;
        private void OnEnable()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _teleportInteraction.PinchStarted += OnPinchStarted;
            _teleportInteraction.PinchEnded += OnPinchEnded;
            _teleportInteraction.InputWentToValid += OnInputWentToValid;
            _teleportInteraction.InputWentToInvalid += OnInputWentToInvalid;

            Hide();
        }

        private void OnDisable()
        {
            _teleportInteraction.PinchStarted -= OnPinchStarted;
            _teleportInteraction.PinchEnded -= OnPinchEnded;

            Hide();
        }


        private void OnPinchStarted()
        {
            Show();
        }

        private void OnPinchEnded()
        {
            Hide();
        }

        private void OnInputWentToInvalid()
        {
            _lineRenderer.enabled = false;
        }

        private void OnInputWentToValid()
        {
            _lineRenderer.enabled = true;
        }

        private void Show()
        {
            _lineRenderer.material = _activeMaterial;
 
        }

        private void Hide()
        {
            _lineRenderer.material = _inactiveMaterial;
        }


        void Update()
        {
            UpdateLineRendererPositions();
        }

        private void UpdateLineRendererPositions()
        {

            Vector3 origin = _teleportInteraction.GetRayOrigin();
            Vector3 target = _teleportInteraction.CurrentRaycastHit.point;
            Vector3 lineRendererTip = origin + (target - origin).normalized * _lineRendererLength;
            _lineRenderer.SetPositions(new Vector3[] { origin, lineRendererTip });
        }
    }
}

