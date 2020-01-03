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
        [Range(0.001f, 10f)] public float minRate = 1.0f;
        [Range(0.001f, 10f)] public float maxRate = 1.0f;
        private float _lastRateChanged = 1.0f;
        public int number = 5;
        public bool infinite = true;
        private int _remainingObjects;

        [Header("LOCATIONS")]
        public GameArea area;
        private Transform _player;
        public float minDistanceFromPlayer;

        [Header("VELOCITY")]
        [Range(-180, 180)] public float angle;
        [Range(0, 360f)] public float spread = 360f;
        [Range(0, 10)] public float minStrength = 1f;
        [Range(0, 10)] public float maxStrength = 10f;
        private float _lastStrengthChanged = -1;

        [Header("ANIMATOR")]
        public string animatorName = "Spawning";
        public float animatorDelayIn = 1;
        public float animatorDelayOut = 1;
        private int _animatorHashId;
        private Animator _animator;

        #endregion

        [UsedImplicitly]
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_animator) _animatorHashId = Animator.StringToHash(animatorName);
        }

        [UsedImplicitly]
        private IEnumerator Start()
        {
            _remainingObjects = number;

            if (minDistanceFromPlayer > 0)
            {
                GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                if (playerGO)
                    _player = playerGO.transform;
                else
                    Debug.LogWarning("No player found.");
            }

            if (_animator)
            {
                _animator.SetBool(_animatorHashId, true);
                yield return new WaitForSeconds(animatorDelayIn);
            }

            while (infinite || _remainingObjects > 0)
            {
                Vector3 position = area ? area.GetRandomPosition() : transform.position;

                if (_player && Vector3.Distance(position, _player.position) < minDistanceFromPlayer)
                    position = (position - _player.position).normalized * minDistanceFromPlayer;

                GameObject obj = Instantiate(reference, position, transform.rotation);
                Rigidbody2D rigidBody = obj.GetComponent<Rigidbody2D>();
                if (rigidBody)
                {
                    float angleDelta = Random.Range(-spread * 0.5f, spread * 0.5f);
                    float angle_ = angle + angleDelta;
                    Vector2 direction = new Vector2(
                        Mathf.Sin(Mathf.Deg2Rad * angle_),
                        Mathf.Cos(Mathf.Deg2Rad * angle_)
                    );
                    direction *= Random.Range(minStrength, maxStrength);
                    rigidBody.velocity = direction;
                }

                _remainingObjects--;

                yield return new WaitForSeconds(1 / Random.Range(minRate, maxRate));
            }

            if (_animator)
            {
                _animator.SetBool(_animatorHashId, false);
                yield return new WaitForSeconds(animatorDelayOut);
            }
        }

        [UsedImplicitly]
        private void OnValidate()
        {
            if (Math.Abs(_lastRateChanged - minRate) > 0.01 && minRate > maxRate)
                _lastRateChanged = maxRate = minRate;
            else if (Math.Abs(_lastRateChanged - maxRate) > 0.01 && maxRate < minRate)
                _lastRateChanged = minRate = maxRate;

            if (Math.Abs(_lastStrengthChanged - minStrength) > 0.01 && minStrength > maxStrength)
                _lastStrengthChanged = maxStrength = minStrength;
            else if (Math.Abs(_lastStrengthChanged - maxStrength) > 0.01 && maxStrength < minStrength)
                _lastStrengthChanged = minStrength = maxStrength;
        }
    }
}
