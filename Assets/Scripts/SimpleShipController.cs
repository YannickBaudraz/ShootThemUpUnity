using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class SimpleShipController : MonoBehaviour
    {
        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// </summary>
        [UsedImplicitly]
        private void Awake()
        {
        }

        /// <summary>
        /// This method is called before the first frame update.
        /// </summary>
        [UsedImplicitly]
        private void Start()
        {
        }

        /// <summary>
        /// This method is called once per frame.
        /// </summary>
        [UsedImplicitly]
        private void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            transform.Rotate(0, 0, -x);
            transform.Translate(0, y, 0);
        }
    }
}