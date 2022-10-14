using UnityEngine;
using UnityEngine.UI;

namespace Xrtinkr.UI
{
    public class DwellFeedback : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dwellFeedbackPrefab;

        private Image _dwellImage;

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(_dwellFeedbackPrefab);
            _dwellImage.GetComponentInChildren<Image>();

        }

        private void Update()
        {
            _dwellImage.transform.position = transform.position;
            
        }

    }

}
