using UnityEngine;

namespace Xrtinkr.Data
{
    public class GLTFParser
    {
        private GameObject _gltfWrapper;

        private const float RESCALING_THRESHOLD = 15;
        private Transform[] _wrapperTransforms => GetWrapperTransforms();
        private Vector3 _minBounds => GetMinBoundary();
        private Vector3 _maxBounds => GetMaxBoundary();

        public GLTFParser(GameObject gltfWrapper)
        {
            _gltfWrapper = gltfWrapper;
          /*  _wrapperTransforms = GetWrapperTransforms();
            _minBounds = GetMinBoundary();
            _maxBounds = GetMaxBoundary();*/
        }

        public void AdjustToGroundLevel()
        {
            float minY = _minBounds.y;
            float yOffset = -minY;

            Vector3 oldPosition = _gltfWrapper.transform.position;
            Vector3 newPosition = oldPosition + new Vector3(0, yOffset, 0);

            _gltfWrapper.transform.position = newPosition;
        }

        private Vector3 GetMinBoundary()
        {
            Vector3 minBounds = Vector3.positiveInfinity;
            foreach (Transform _transform in _wrapperTransforms)
            {

                if (_transform.position == Vector3.zero)
                {
                    continue;
                }
                float x = _transform.position.x;
                float y = _transform.position.y;
                float z = _transform.position.z;

                if (x < minBounds.x)
                {
                    minBounds.x = x;
                }

                if (y < minBounds.y)
                {
                    minBounds.y = y;
                }

                if (z < minBounds.z)
                {
                    minBounds.z = z;
                }
            }

            return minBounds;
        }
        private Vector3 GetMaxBoundary()
        {
     
            Vector3 maxBounds = Vector3.negativeInfinity;
            foreach(Transform _transform in _wrapperTransforms)
            {
     
                if(_transform.position == Vector3.zero)
                {
                    continue;
                }
                float x = _transform.position.x;
                float y = _transform.position.y;
                float z = _transform.position.z;


                if (x > maxBounds.x)
                {
                    maxBounds.x = x;
                }

                if (y > maxBounds.y)
                {
                    maxBounds.y = y;
                }

                if (z > maxBounds.z)
                {
                    maxBounds.z = z;
                }
            }
     
            return maxBounds;
 
        }

        private Transform[] GetWrapperTransforms() => _gltfWrapper.GetComponentsInChildren<Transform>();

        public void RescaleIfRequired()
        {
            Vector3 minBound = _minBounds;
            Vector3 maxBound = _maxBounds;

            float distance = Vector3.Distance(minBound, maxBound);

            if(distance > RESCALING_THRESHOLD)
            {
                Rescale();
            }
        }

        private void Rescale()
        {
            float scalingFactor = Vector3.Distance(_minBounds, _maxBounds) / 4;
            _gltfWrapper.transform.localScale /= scalingFactor;
        }

        public void Center()
        {
            Vector3 sceneOrigin = Vector3.zero;
            Vector3 centerOfMass = GetCenterOfMass();

            foreach (Transform _transform in _wrapperTransforms)
            {
                Vector3 fromCenterOfMass = centerOfMass - _transform.position;
                _transform.position = fromCenterOfMass;
            }
        }

        private Vector3 GetCenterOfMass() => (_minBounds + _maxBounds) / 2;
    }
}

