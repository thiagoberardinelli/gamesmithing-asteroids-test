using UnityEngine;

namespace _Project.Scripts.Behaviours
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rgbd;
        [SerializeField] private float thrust;
        [SerializeField] private float rotationForce;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private GameObject bulletPrefab;
        
        private readonly float _maxLimitSpeed = 5F;
        private readonly float _fireRate = 0.2F;
        private float _nextBullet;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || Time.time < _nextBullet) return;

            _nextBullet = Time.time + _fireRate;
            
            Fire();
        }

        private void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Mathf.Clamp01(Input.GetAxis("Vertical"));
            
            Rotate(horizontalInput);
            Move(verticalInput);
        }

        private void Move(float verticalInput)
        {
            rgbd.AddRelativeForce(Vector3.up * thrust * verticalInput, ForceMode2D.Force);

            Vector2 clampedVelocity = new Vector2(Mathf.Clamp(rgbd.velocity.x, -_maxLimitSpeed, _maxLimitSpeed),
                Mathf.Clamp(rgbd.velocity.y, -_maxLimitSpeed, _maxLimitSpeed));

            rgbd.velocity = clampedVelocity;
        }

        private void Rotate(float horizontalInput)
        {
            transform.Rotate(0, 0, -horizontalInput * rotationForce * Time.deltaTime);
        }

        private void Fire()
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Asteroid")) return;
            Destroy(gameObject);
        }
    }
}
