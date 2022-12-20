
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace XRTinkr.UI.HandUI
{
    public class HandUI : MonoBehaviour
    {

        [SerializeField]
        private Transform _handAnchor;

        [SerializeField]
        private Hand _hand;

        private Transform _head;

        [SerializeField]
        private GameObject _toggleTarget;

        private bool _uiEnabled = false;

        private void Awake()
        {
            _head = Camera.main.transform;
        }

        void Update()
        {
            UpdateUITransform();

            Pose pose;
            _hand.GetRootPose(out pose);

            Pose palmPose;
            _hand.GetPalmPoseLocal(out palmPose);

            Vector3 toHead = (_head.position - palmPose.position).normalized;

            float angle = Vector3.Angle(toHead, palmPose.forward);
            Debug.Log("Angle: " + angle);
            if (angle < 30)
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

            /*Vector3 uiLookDir = -_followTarget.up;
            Vector3 lookDir = _head.forward;
            float angle = Vector3.Angle(lookDir, uiLookDir);
            Debug.Log(angle);

            if (angle < 30)
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
            }*/

        }

        private void UpdateUITransform()
        {

            
            Vector3 fromUser = transform.position - _head.position;
            Vector3 offset = fromUser * 0.2f;
            transform.position = Vector3.Lerp(transform.position, _handAnchor.position - offset, 0.08f);
   
            transform.forward = fromUser;
        }

        private void EnableUI()
        {
            //gameObject.SetActive(true);
            _toggleTarget.SetActive(true);
            _uiEnabled = true;
        }

        private void DisableUI()
        {
            //gameObject.SetActive(false);
            _toggleTarget.SetActive(false);

            _uiEnabled = false;

        }
    }
}

