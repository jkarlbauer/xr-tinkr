using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Xrtinkr.Interaction
{
    [RequireComponent(typeof(PokeInteractable))]
    public class PokeInteractionDetector : MonoBehaviour
    {
        private PokeInteractable pokeInteractable;

        public UnityEvent OnButtonHover;
        public UnityEvent OnButtonSelect;
 
        private void Awake()
        {
            pokeInteractable = GetComponent<PokeInteractable>();
            pokeInteractable.WhenStateChanged += OnStateChanged;
        }

        private void OnStateChanged(InteractableStateChangeArgs obj)
        {
        
            if(obj.NewState == InteractableState.Select)
            {
                UnityEngine.Debug.Log("Button Pressed");
                OnButtonSelect.Invoke();
            }

            if(obj.NewState == InteractableState.Hover)
            {
                OnButtonHover.Invoke();
            }

        }
    }
}

