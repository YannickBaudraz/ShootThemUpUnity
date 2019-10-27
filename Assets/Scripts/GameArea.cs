using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represent the area of the game.
    /// </summary>
    [AddComponentMenu("Yannick Baudraz/Game Area")]
    public class GameArea : MonoBehaviour
    {
        public Rect Area { get; private set; } = new Rect(0, 0, 10, 10);
        public Vector2 size;
        public Color gizmoColor = new Color(0, 0, 1, 0.3f);
        private Color _gizmoWireColor = new Color(0, 0, 1, 0.3f);

        public void SetArea(Vector2 size) => Area = new Rect(size.x * -0.5f, size.y * -0.5f, size.x, size.y);

        [UsedImplicitly]
        private void OnValidate()
        {
            SetArea(size);
            _gizmoWireColor = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b);
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(
                new Vector3(Area.center.x, Area.center.y, 0),
                new Vector3(Area.width, Area.height, 0)
            );

            Gizmos.color = _gizmoWireColor;
            Gizmos.DrawWireCube(
                new Vector3(Area.center.x, Area.center.y, 0),
                new Vector3(Area.width, Area.height, 0)
            );
        }
    }
}