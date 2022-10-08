using System;
using UnityEngine;

namespace Xrtinkr.Debug
{
    public class FollowHeadPose : MonoBehaviour
    {
        [SerializeField]
        private Transform _head;

        private Vector3 _yOffset;

        private void Start()
        {
            _yOffset = new Vector3(0, -0.5f, 0);
        }
        void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }
        private void UpdatePosition()
        {
            Vector3 _totalPositionOffset = _head.position + _head.transform.forward + _yOffset;
            transform.position = Vector3.Lerp(transform.position, _totalPositionOffset, 0.08f);
        }
        private void UpdateRotation()
        {
            Vector3 _headToHereDirection = transform.position - _head.transform.position;
            transform.forward = Vector3.Lerp(transform.forward, _headToHereDirection, 0.08f);
        }

    }
}

