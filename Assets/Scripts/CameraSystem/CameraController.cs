using UnityEngine;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 5f, -5f);
        [SerializeField] private float smoothSpeed = 5f;

        private void LateUpdate()
        {
            if (!target) return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            var targetPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}