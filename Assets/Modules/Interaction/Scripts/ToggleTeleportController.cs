using UnityEngine;

namespace Xrtinkr.Interaction{
    public class ToggleTeleportController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _teleportController;

        public void Toggle()
        {
            if (_teleportController.activeInHierarchy)
            {
                DisableController();
            }
            else
            {
                EnableController();
            }
        }

        private void EnableController()
        {
            _teleportController.SetActive(true);
        }

        private void DisableController()
        {
            _teleportController.SetActive(false);
        }
    }
}

