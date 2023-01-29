using System;
using UnityEngine;

namespace _Project.Scripts
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
            CheckScreenBounds();
            
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
        
        private void CheckScreenBounds()
        {
            if (Camera.main == null) return;
            
            Vector3 viewPortPoint = Camera.main.WorldToViewportPoint(transform.position);
        
            if (viewPortPoint.x < 0)
                viewPortPoint = new Vector3(1F,  viewPortPoint.y, viewPortPoint.z);
            else if (viewPortPoint.x >= 1.05)
                viewPortPoint = new Vector3(0F, viewPortPoint.y, viewPortPoint.z);
            
            if (viewPortPoint.y < 0)
                viewPortPoint = new Vector3(viewPortPoint.x,  1F, viewPortPoint.z);
            else if (viewPortPoint.y >= 1.05F)
                viewPortPoint = new Vector3(viewPortPoint.x, 0F, viewPortPoint.z);
            
            transform.position = Camera.main.ViewportToWorldPoint(viewPortPoint);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Asteroid")) return;
            Destroy(gameObject);
        }
    }
}
