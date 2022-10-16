using UnityEngine;
using Xrtinkr.Interaction;

namespace Xrtinkr.UI.Teleport
{
    public class TeleportReticleVisuals : MonoBehaviour
    {
        [SerializeField]
        private TeleportInteraction _teleportInteraction;

        private void OnEnable()
        {

            _teleportInteraction.PinchStarted += OnPinchStarted;
            _teleportInteraction.PinchEnded += OnPinchEnded;
        }

        private void OnDisable()
        {
            _teleportInteraction.PinchStarted -= OnPinchStarted;
            _teleportInteraction.PinchEnded -= OnPinchEnded;
        }


        private void OnPinchStarted()
        {
            Show();
        }

        private void OnPinchEnded()
        {
            Hide();
        }

        private void Show()
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;

        }

        private void Hide()
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;

        }

        void Update()
        {
            UpdateReticlePosition();
        }

        private void UpdateReticlePosition()
        {
            transform.position = _teleportInteraction.CurrentRaycastHit.point;
        }
    }

}
