using UnityEngine;
using Oculus.Interaction;
using System;
using System.Collections;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(IInteractor))]
    public class TogglePassthrough : MonoBehaviour
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
            if (OVRManager.IsInsightPassthroughInitialized())
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
            OVRManager.instance.isInsightPassthroughEnabled = true;
            ConfigurePassthroughLayer();
        }

        private void DisablePassthrough()
        {
            OVRManager.instance.isInsightPassthroughEnabled = false;
        }

        private void ConfigurePassthroughLayer()
        { 
            _passthroughLayer.textureOpacity = 0;
            _passthroughLayer.edgeRenderingEnabled = true;
            _passthroughLayer.edgeColor = new Color(0, 200, 255, 200);
            Debug.Log("Configured Passthrough Layer");
        }

    }
}

