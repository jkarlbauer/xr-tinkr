using System;
using UnityEngine;

namespace Xrtinkr.Debug
{
    public class FollowHeadPose : MonoBehaviour
    {
        private Transform _head;

        [SerializeField]
        private float _yOffset;

        private void Awake()
        {
            _head = Camera.main.transform;
        }
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
            Vector3 _fromHead = transform.position - _head.transform.position;
            transform.forward = Vector3.Lerp(transform.forward, _fromHead, 0.08f);
        }

    }
}

