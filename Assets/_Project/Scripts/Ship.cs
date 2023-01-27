using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rgbd;
        [SerializeField] private float thrust;
        [SerializeField] private float rotationForce;
        [SerializeField] private Transform laserSpawnTransform;

        private const float MaxLimitSpeed = 5F;
        
        private void Update()
        {
            
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

            Vector2 clampedVelocity = new Vector2(Mathf.Clamp(rgbd.velocity.x, -MaxLimitSpeed, MaxLimitSpeed),
                Mathf.Clamp(rgbd.velocity.y, -MaxLimitSpeed, MaxLimitSpeed));

            rgbd.velocity = clampedVelocity;
        }

        private void Rotate(float horizontalInput)
        {
            transform.Rotate(0, 0, -horizontalInput * rotationForce * Time.deltaTime);
        }
    }
}
