using UnityEngine;
using UnityEngine.UI;
using Xrtinkr.Interaction;

namespace Xrtinkr.UI.Teleport
{
    public class TeleportDwellVisuals : MonoBehaviour
    {
        [SerializeField]
        private TeleportInteraction _teleportInteraction;

        [SerializeField]
        private Transform _lookTarget;

        private Image _dwellImage;

        private void OnEnable()
        {
            _dwellImage = GetComponentInChildren<Image>();
            _teleportInteraction.InteractionStarted += OnInteractionStarted;
            _teleportInteraction.InteractionEnded += OnInteractionEnded;
            Hide();
        }

        private void OnDisable()
        {
            _teleportInteraction.InteractionStarted -= OnInteractionStarted;
            _teleportInteraction.InteractionEnded -= OnInteractionEnded;
            Hide();
        }
        private void OnInteractionStarted()
        {
            Show();
        }

        private void OnInteractionEnded()
        {
            Hide();
        }

        public void Show() => _dwellImage.enabled = true;
        public void Hide() => _dwellImage.enabled = false;

        private void Update()
        {
            UpdateOrienteation();
            UpdatePosition();
            SetDwellFill(_teleportInteraction.GetCurrentPinchingDuration());
        }

        public void SetDwellFill(float amount)
        {
            if(amount != _dwellImage.fillAmount)
            {
                _dwellImage.fillAmount = amount;
            }
        }

        private void UpdateOrienteation() => transform.forward = transform.position - _lookTarget.position;
        public void UpdatePosition() => transform.position = _teleportInteraction.GetRayOrigin();

    }

}

