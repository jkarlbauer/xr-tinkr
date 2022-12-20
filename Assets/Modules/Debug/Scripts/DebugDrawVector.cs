using System.Collections.Generic;
using UnityEngine;

namespace Xrtinkr.Debug
{
    public class DebugVector
    {
        public Vector3 from;
        public Vector3 to;
        public Vector3 Direction => to - from;

        public DebugVector(Vector3 from, Vector3 to)
        {
            this.from = from;
            this.to = to;
        }
    }

    public static class DebugDrawVector
    {
        private static Queue<DebugVector> vectors = new Queue<DebugVector>();
        public static bool vectorsInQueue => vectors.Count > 0;
        public static void Draw(Vector3 from, Vector3 to) => vectors.Enqueue(new DebugVector(from, to));

        public static DebugVector Dequeue() => vectors.Dequeue();
    }

}
