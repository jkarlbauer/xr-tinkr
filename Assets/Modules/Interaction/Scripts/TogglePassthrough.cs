
using UnityEngine;

namespace Xrtinkr.Interaction
{
    public class TogglePassthrough : MonoBehaviour
    {

        [SerializeField]
        private OVRPassthroughLayer _passthroughLayer;

        public void TogglePassthroughState()
        {
            if (_passthroughLayer.enabled)
            {
                DisablePassthrough();
            }
            else
            {
                EnablePassthrough();
            }
        }

        private void EnablePassthrough()
        {

            _passthroughLayer.enabled = true;
            UnityEngine.Debug.Log("enabled passthrough");
        }

        private void DisablePassthrough()
        {
            _passthroughLayer.enabled = false;
            UnityEngine.Debug.Log("disabled passthrough");

        }
    }
}


