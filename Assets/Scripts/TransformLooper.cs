using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Loop the position of the object in a rectangular area.
    /// </summary>
    /// 
    /// <remarks>
    /// For exemple : When the object exits the right edge,
    /// it returns to the left edge.
    /// </remarks>
    [AddComponentMenu("Yannick Baudraz/Transform Looper")]
    public class TransformLooper : MonoBehaviour
    {
        public GameArea _gameArea;
        private Vector3 _areaSpacePosition;

        [UsedImplicitly]
        private void Update()
        {
            _areaSpacePosition = _gameArea.transform.InverseTransformPoint(transform.position);

            if (_gameArea.Area.Contains(_areaSpacePosition)) return;

            if (_areaSpacePosition.x < _gameArea.Area.xMin) _areaSpacePosition.x = _gameArea.Area.xMax;
            else if (_areaSpacePosition.x > _gameArea.Area.xMax) _areaSpacePosition.x = _gameArea.Area.xMin;

            if (_areaSpacePosition.y < _gameArea.Area.yMin) _areaSpacePosition.y = _gameArea.Area.yMax;
            else if (_areaSpacePosition.y > _gameArea.Area.yMax) _areaSpacePosition.y = _gameArea.Area.yMin;

            transform.position = _gameArea.transform.TransformPoint(_areaSpacePosition);
        }
    }
}