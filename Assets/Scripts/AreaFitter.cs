using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    [AddComponentMenu("Yannick Baudraz/ Fit Area To Camera")]
    [RequireComponent(typeof(GameArea))]
    public class AreaFitter : MonoBehaviour
    {
        private GameArea Area => GetComponent<GameArea>();

        [UsedImplicitly]
        private void Awake() => FitToMainCamera();

        private void FitToCamera(Camera camera)
        {
            Area.Size = new Vector2(camera.aspect * camera.orthographicSize * 2, camera.orthographicSize * 2);
            transform.position = camera.transform.position;
            transform.rotation = camera.transform.rotation;
        }

        private void FitToMainCamera() => FitToCamera(Camera.main);

        [UsedImplicitly]
        private void Reset() => FitToMainCamera();
    }
}