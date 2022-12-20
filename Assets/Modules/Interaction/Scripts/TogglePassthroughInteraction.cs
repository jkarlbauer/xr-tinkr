using UnityEngine;
using Oculus.Interaction;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(IInteractor))]
    public class TogglePassthroughInteraction : MonoBehaviour
    {
        [SerializeField]
        private OVRPassthroughLayer _passthroughLayer;

        private IInteractor _interactor;

        private void OnEnable()
        {
            _interactor = GetComponent<IInteractor>();
            _interactor.WhenStateChanged += ProcessState;
        }

        private void ProcessState(InteractorStateChangeArgs obj)
        { 
            if(obj.NewState == InteractorState.Select)
            {
                TogglePassthroughState();
            }
        }

        private void TogglePassthroughState()
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

