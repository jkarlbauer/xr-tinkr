
using UnityEngine;
using UnityEngine.UI;

namespace XRTinkr.UI.HandUI
{
    public class HandUIToggle : MonoBehaviour
    {

        [SerializeField]
        private Transform _handAnchor;

        private Transform _head;

        [SerializeField]
        private Image _toggleTarget;

        private bool _uiEnabled = false;

        private void Awake()
        {
            _head = Camera.main.transform;
        }

        void Update()
        {
            Vector3 uiLookDir = -_handAnchor.up;
            Vector3 lookDir = _head.forward;
            float angle = Vector3.Angle(lookDir, uiLookDir);
            Debug.Log(angle);

            if (angle < 20)
            {
                if (_uiEnabled)
                {
                    return;
                }

                EnableUI();
            }
            else
            {
                if (!_uiEnabled)
                {
                    return;
                }

                DisableUI();
            }

        }

        private void EnableUI()
        {
            //gameObject.SetActive(true);
            _toggleTarget.enabled = true;
            _uiEnabled = true;
        }

        private void DisableUI()
        {
            //gameObject.SetActive(false);
            _toggleTarget.enabled = false;

            _uiEnabled = false;

        }
    }
}

