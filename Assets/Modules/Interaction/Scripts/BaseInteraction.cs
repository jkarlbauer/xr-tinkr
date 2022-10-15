using Oculus.Interaction;
using UnityEngine;

[RequireComponent(typeof(IInteractor))]
public abstract class BaseInteraction : MonoBehaviour
{
    protected IInteractor interactor;

    protected void RegisterInteractor()
    {
        interactor = GetComponent<IInteractor>();
        interactor.WhenStateChanged += ProcessState;       
    }

    protected void ProcessState(InteractorStateChangeArgs obj)
    {
        if (HasPinchStarted(obj))
        {
            HandlePinchStarted();
        }

        if (HasPinchEnded(obj))
        {
            HandlePinchEnded();
        }
    }

    protected virtual void HandlePinchStarted(){ }

    protected virtual void HandlePinchEnded() { }

    private bool HasPinchStarted(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Hover && obj.NewState == InteractorState.Select;
    private bool HasPinchEnded(InteractorStateChangeArgs obj) => obj.PreviousState == InteractorState.Select && obj.NewState == InteractorState.Hover;


}
