using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        #region Attributes

        [Header("SPAWN")]
        public GameObject reference;

        [Header("SPAWNING")]
        [Range(0.001f, 100f)] public float minRate = 1.0f;
        [Range(0.001f, 100f)] public float maxRate = 1.0f;
        public int number;
        public bool infinite;

        private int _remainingObjects;

        [Header("LOCATIONS")]
        public GameArea area;

        #endregion

        [UsedImplicitly]
        private IEnumerator Start()
        {
            _remainingObjects = number;

            while (infinite || _remainingObjects > 0)
            {
                Vector3 position = area ? area.GetRandomPosition() : transform.position;

                Instantiate(reference, position, transform.rotation);
                _remainingObjects--;

                yield return new WaitForSeconds(1 / Random.Range(minRate, maxRate));
            }
        }
    }
}
