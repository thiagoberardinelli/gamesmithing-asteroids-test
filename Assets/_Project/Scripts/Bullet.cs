using UnityEngine;

namespace _Project.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rgbd;
        [SerializeField] private float speed;
        
        private void Start() => rgbd.AddRelativeForce(Vector3.up * speed, ForceMode2D.Force);

        private void Update()
        {
            if (!IsOffScreen()) return;
            Destroy(gameObject, 0.1F);
        }

        private bool IsOffScreen()
        {
            Vector3 transformViewportPoint = Camera.main.WorldToViewportPoint(transform.position);

            return transformViewportPoint.x > 1 || transformViewportPoint.x < 0 ||
                   transformViewportPoint.y > 1 || transformViewportPoint.y < 0;
        }
    }
}