using System;
using UnityEngine;

namespace Xrtinkr.Debug
{
    public class FollowHeadPose : MonoBehaviour
    {
        [SerializeField]
        private Transform _head;

        [SerializeField]
        private float _yOffset;

        void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }
        private void UpdatePosition()
        {
            Vector3 _totalPositionOffset = _head.position + _head.transform.forward + new Vector3(0, _yOffset, 0);
            transform.position = Vector3.Lerp(transform.position, _totalPositionOffset, 0.08f);
        }
        private void UpdateRotation()
        {
            Vector3 _headToHereDirection = transform.position - _head.transform.position;
            transform.forward = Vector3.Lerp(transform.forward, _headToHereDirection, 0.08f);
        }

    }
}

