using UnityEngine;
using Oculus.Interaction;

[RequireComponent(typeof(RayInteractor))]
[RequireComponent(typeof(LineRenderer))]
public class TeleportInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _reticle;

    [SerializeField]
    private GameObject _OVRCameraRig;

    private RayInteractor _rayInteractor;

    private LineRenderer lineRenderer;

    private void OnEnable()
    {
        _rayInteractor = GetComponent<RayInteractor>();
        _rayInteractor.WhenStateChanged += ProcessState;
        _reticle = Instantiate(_reticle);
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void ProcessState(InteractorStateChangeArgs obj)
    {
        if(obj.NewState == InteractorState.Select)
        {
            _OVRCameraRig.transform.position = _reticle.transform.position;
        }
    }

    private void Update()
    {
        lineRenderer.SetPositions(new Vector3[] { _rayInteractor.Ray.origin, _rayInteractor.Ray.origin + _rayInteractor.Ray.direction });

        RaycastHit hit;
        if (Physics.Raycast(_rayInteractor.Ray.origin, _rayInteractor.Ray.direction, out hit, Mathf.Infinity, LayerMask.GetMask("TeleportTarget")))
        {
            _reticle.transform.position = hit.point;     
        }

 
    }



}
