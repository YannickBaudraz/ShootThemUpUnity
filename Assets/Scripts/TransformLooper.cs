using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Loop the position of the object in a rectangular area
    /// </summary>
    /// 
    /// <remarks>
    /// For exemple : When the object exits the right edge,
    /// it returns to the left edge.
    /// </remarks>
    [AddComponentMenu("Yannick Baudraz/Transform Looper")]
    public class TransformLooper : MonoBehaviour
    {
        private Rect area;

        [UsedImplicitly]
        private void Awake() => area = new Rect(0, 0, 10, 10);

        [UsedImplicitly]
        private void Update()
        {
            Vector3 position = transform.position;

            if (area.Contains(position)) return;

            if (position.x < area.xMin) position.x = area.xMax;
            else if (position.x > area.xMax) position.x = area.xMin;

            if (position.y < area.yMin) position.y = area.yMax;
            else if (position.y > area.yMax) position.y = area.yMin;

            transform.position = position;
        }
    }
}