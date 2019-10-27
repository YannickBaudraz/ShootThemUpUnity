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
        private Vector2 _delta = Vector2.zero;

        [UsedImplicitly]
        private void Awake() => _delta = Vector2.zero;

        [UsedImplicitly]
        private void Update()
        {
            #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR || REMOTE
            int numberOfTouch = Input.touchCount;
            if (numberOfTouch > 0)
            {
                if (numberOfTouch == 1)
                {
                    Touch firstTouch = Input.GetTouch(0);
                    if (firstTouch.phase == TouchPhase.Moved)
                        _delta = new Vector2(
                            firstTouch.deltaPosition.x / (Screen.width * firstTouch.deltaTime),
                            firstTouch.deltaPosition.y / Screen.height / firstTouch.deltaTime / 2
                        );
                }
                else if (numberOfTouch == 2)
                {
                    Touch secondTouch = Input.GetTouch(1);
                    if (secondTouch.phase == TouchPhase.Moved)
                        _delta.x = secondTouch.deltaPosition.x / Screen.width / secondTouch.deltaTime * 5;
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

            transform.Translate(0, _delta.y, 0);
            transform.Rotate(0, 0, -_delta.x);
        }
    }
}