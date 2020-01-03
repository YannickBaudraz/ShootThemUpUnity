using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Represent a rectangular game area.
    /// </summary>
    [AddComponentMenu("Yannick Baudraz/Game Area")]
    public class GameArea : MonoBehaviour
    {
        #region Attributes

        public Rect Area { get; private set; } = new Rect(0, 0, 10, 10);

        public Color gizmoColor = new Color(0, 0, 1, 0.3f);
        private Color _gizmoWireColor = new Color(0, 0, 1, 0.3f);

        [SerializeField]
        internal Vector2 _size = Vector2.zero;
        public Vector2 Size
        {
            get => Area.size;
            set => Area = new Rect(value.x * -0.5f, value.y * -0.5f, value.x, value.y);
        }

        #endregion

        #region Unity methods

        [UsedImplicitly]
        private void OnValidate()
        {
            Size = _size;
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

        #endregion

        public Vector3 GetRandomPosition()
        {
            Vector3 vector3 = new Vector3(
                Random.Range(Area.xMin, Area.xMax),
                Random.Range(Area.yMin, Area.yMax)
            );
            vector3 = transform.TransformPoint(vector3);

            return vector3;
        }
    }
}
