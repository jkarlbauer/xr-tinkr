using UnityEngine;

namespace Xrtinkr.Debug
{
    public class FollowHeadPose : MonoBehaviour
    {
        [SerializeField]
        private Transform head;

        void Update()
        {
            Vector3 verticalOffset = new Vector3(0, -0.5f, 0);
            Vector3 positionOffsetFromHead = head.position + head.transform.forward + verticalOffset;
            transform.position = Vector3.Lerp(transform.position, positionOffsetFromHead, 0.08f);
            Vector3 fromHeadToHere = transform.position - head.position;
            transform.forward = Vector3.Lerp(transform.forward, fromHeadToHere, 0.08f);
        }
    }
}

