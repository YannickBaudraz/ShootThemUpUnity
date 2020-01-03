using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private float lastRateChanged = 1.0f;
        public int number;
        public bool infinite;

        private int _remainingObjects;

        [Header("LOCATIONS")]
        public GameArea area;
        private Transform player;
        public float minDistanceFromPlayer;

        #endregion

        [UsedImplicitly]
        private IEnumerator Start()
        {
            _remainingObjects = number;

            if (minDistanceFromPlayer > 0)
            {
                GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                if (playerGO) 
                    player = playerGO.transform;
                else 
                    Debug.LogWarning("No player found.");
            }

            while (infinite || _remainingObjects > 0)
            {
                Vector3 position = area ? area.GetRandomPosition() : transform.position;

                if (player && Vector3.Distance(position, player.position) < minDistanceFromPlayer)
                    position = (position - player.position).normalized * minDistanceFromPlayer;

                Instantiate(reference, position, transform.rotation);
                
                _remainingObjects--;

                yield return new WaitForSeconds(1 / Random.Range(minRate, maxRate));
            }
        }

        [UsedImplicitly]
        public void OnValidate()
        {
            if (Math.Abs(lastRateChanged - minRate) > 0.1 && minRate > maxRate) lastRateChanged = maxRate = minRate;

            if (Math.Abs(lastRateChanged - maxRate) > 0.1 && maxRate < minRate) lastRateChanged = minRate = maxRate;
        }
    }
}
