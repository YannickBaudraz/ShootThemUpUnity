using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Control the star ship. 
    /// </summary>
    /// 
    /// <remarks>
    /// The ship can be controlled with the keyboard in a computer
    /// or with the touch screen in a smartphone.
    /// </remarks>
    [AddComponentMenu("Yannick Baudraz/Simple Ship Controller")]
    public class SimpleShipController : MonoBehaviour
    {
        #region Attributes

        public float thrustPower = 1;
        public float steerPower = 1;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _delta = Vector2.zero;
        private Vector2 _force = Vector2.zero;
        private float _torque;

        #endregion

        #region Unity methods

        [UsedImplicitly]
        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        [UsedImplicitly]
        private void FixedUpdate()
        {
            #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR || REMOTE
            int numberOfTouch = Input.touchCount;
            if (numberOfTouch > 0)
            {
                switch (numberOfTouch)
                {
                    default:
                        Touch firstTouch = Input.GetTouch(0);
                        if (firstTouch.phase == TouchPhase.Moved)
                            _delta.y = firstTouch.deltaPosition.y / Screen.height / firstTouch.deltaTime / 2;
                        break;
                    case 2:
                        Touch secondTouch = Input.GetTouch(1);
                        if (secondTouch.phase == TouchPhase.Moved)
                            _delta.x = secondTouch.deltaPosition.x / Screen.width / secondTouch.deltaTime / 2;
                        break;
                }
            }
            else
            {
                _delta = Vector2.zero;
            }
            #else
            _delta.x = Input.GetAxis("Horizontal");
            _delta.y = Input.GetAxis("Vertical");
            #endif

            _force.y = _delta.y * thrustPower;
            _torque = -_delta.x * steerPower;
            _rigidbody2D.AddRelativeForce(_force);
            _rigidbody2D.AddTorque(_torque);
        }

        #endregion
    }
}
