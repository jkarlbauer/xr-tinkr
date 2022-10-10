using UnityEngine;
using Oculus.Interaction;
using System;
using System.Collections;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(IInteractor))]
    public class TogglePassthrough : MonoBehaviour
    {
        private IInteractor interactor;

        private void OnEnable()
        {
            interactor = GetComponent<IInteractor>();
        }

        void Start()
        {

            interactor.WhenStateChanged += ProcessState;

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
            if (OVRManager.IsInsightPassthroughInitialized())
            {
                DisablePassthrough();
            }
            else
            {
                EnablePassthrough();
            }
        }

        private void EnablePassthrough() => OVRManager.instance.isInsightPassthroughEnabled = true;

        private void DisablePassthrough() => OVRManager.instance.isInsightPassthroughEnabled = false;

    }
}

