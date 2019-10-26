using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Control the star ship
    /// </summary>
    /// 
    /// <remarks>
    /// The ship can be controlled with the keyboard in a computer
    /// or with the touch screen in a smartphone.
    /// </remarks>
    [AddComponentMenu("Yannick Baudraz/Simple Ship Controller")]
    public class SimpleShipController : MonoBehaviour
    {
        private Vector2 delta;

        [UsedImplicitly]
        private void Awake() => delta = Vector2.zero;

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
                        delta = new Vector2(
                            firstTouch.deltaPosition.x / (Screen.width * firstTouch.deltaTime),
                            firstTouch.deltaPosition.y / Screen.height / firstTouch.deltaTime / 2
                        );
                }
                else if (numberOfTouch == 2)
                {
                    Touch secondTouch = Input.GetTouch(1);
                    if (secondTouch.phase == TouchPhase.Moved)
                        delta.x = secondTouch.deltaPosition.x / Screen.width / secondTouch.deltaTime * 5;
                }
            }
            else
            {
                delta = Vector2.zero;
            }
            #else
            delta.x = Input.GetAxis("Horizontal");
            delta.y = Input.GetAxis("Vertical");
            #endif

            transform.Translate(0, delta.y, 0);
            transform.Rotate(0, 0, -delta.x);
        }
    }
}