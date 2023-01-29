using UnityEngine;

namespace _Project.Scripts.Behaviours
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rgbd;

        private int _splitsLeft = 2;
        private float _initialSpeed = 50F;
        private int _maxFragmentCount = 3;

        private void Start()
        {
            if (_splitsLeft >= 2)
                rgbd.AddRelativeForce(GetRandomVector2() * _initialSpeed, ForceMode2D.Force);

            rgbd.AddTorque(Random.Range(3F, 10F));
        }

        private void SetUp(int splitsLeft, float initialSpeed)
        {
            transform.localScale /= 1.5F;
            _splitsLeft = splitsLeft;
            _initialSpeed = initialSpeed;

            rgbd.AddRelativeForce(GetRandomVector2() * _initialSpeed, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Bullet")) return;
            
            if (_splitsLeft == 0) 
                Destroy(gameObject);
            else 
                Split();
        }

        private void Split()
        {
            for (int i = 0; i < Random.Range(2, _maxFragmentCount); i++)
            {
                Asteroid asteroidInstance = Instantiate(this, transform.position, transform.rotation);
                asteroidInstance.SetUp(_splitsLeft - 1, _initialSpeed * 1.25F);
            }
            
            Destroy(gameObject);
        }

        private Vector2 GetRandomVector2() => Random.insideUnitCircle.normalized;
    }
}