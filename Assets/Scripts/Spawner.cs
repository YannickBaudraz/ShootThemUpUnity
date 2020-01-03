using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [Header("SPAWN")]
        public GameObject reference;

        [Header("SPAWNING")]
        [Range(0.001f, 100f)] public float rate = 1.0f;
        public int number;
        public bool infinite;

        private int _remainingObjects;

        [UsedImplicitly]
        private IEnumerator Start()
        {
            _remainingObjects = number;

            while (infinite || _remainingObjects > 0)
            {
                Instantiate(reference, transform.position, transform.rotation);
                _remainingObjects--;

                yield return new WaitForSeconds(1 / rate);
            }
        }
    }
}
