using UnityEngine;

namespace Xrtinkr.Debug
{
    public class VectorDebugger : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vectorPrefab;


        void Update()
        {
            if (!DebugDrawVector.vectorsInQueue)
            {
                return;
            }

            DrawVector(DebugDrawVector.Dequeue());
        }

        private void DrawVector(DebugVector vector)
        {
            GameObject vectorObject = Instantiate(_vectorPrefab);
            vectorObject.transform.position = vector.from;
            vectorObject.transform.up = vector.Direction;
            Destroy(vectorObject, Time.deltaTime + 0.001f);
            
        }
    }
}

