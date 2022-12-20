
using UnityEngine;

namespace XRTinkr.UI.HandUI
{
    public class HandUI : MonoBehaviour
    {
        [SerializeField]
        private Transform _handAnchor;

        [SerializeField]
        private GameObject _toggleTarget;

        private Transform _head;

        private bool _uiEnabled;

        private void Awake()
        {
            _head = Camera.main.transform;
            _uiEnabled = false;
        }

        void Update()
        {
            UpdateUITransform();
            UpdateUIVisibility();
        }

        private void UpdateUITransform()
        {
            Vector3 fromUser = transform.position - _head.position;
            Vector3 offset = fromUser * 0.2f;
            transform.position = Vector3.Lerp(transform.position, _handAnchor.position - offset, 0.08f);
            transform.forward = fromUser;
        }

        private void UpdateUIVisibility()
        {
            Vector3 toHead = (_head.position - _handAnchor.position).normalized;
            float angle = Vector3.Angle(toHead, _handAnchor.up);

            if (angle < 30)
            {
                EnableUI();
                return;
            }
     
            DisableUI();
        }

        private void EnableUI()
        {
            if (_uiEnabled)
            {
                return;
            }

            _toggleTarget.SetActive(true);
            _uiEnabled = true;
        }

        private void DisableUI()
        {
            if (!_uiEnabled)
            {
                return;
            }

            _toggleTarget.SetActive(false);
            _uiEnabled = false;
        }
    }
}

