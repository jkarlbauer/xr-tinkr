
using UnityEngine;
using UnityEngine.UI;

namespace Xrtinkr.UI
{
    public class DwellTimer : MonoBehaviour
    {
        private Image _dwellImage;
        private Transform _orientationTarget;

        public void Initialize()
        {
            _dwellImage = gameObject.GetComponentInChildren<Image>();
            Hide();

        }
        private float _dwellTime;

        public float DwellTime
        {
            get => _dwellTime;

            set => _dwellTime = value;

        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetDwellFill(float amount)
        {
            _dwellImage.fillAmount = amount;
        }
        public void SetOrientationTarget(Transform target) => _orientationTarget = target;

        private void Update()
        {
            if(_orientationTarget != null)
            {
                UpdateOrienteation();

            }
        }

        private void UpdateOrienteation() => transform.forward = transform.position - _orientationTarget.position;
        public void UpdatePosition(Vector3 position) => transform.position = position;        

    }

}
