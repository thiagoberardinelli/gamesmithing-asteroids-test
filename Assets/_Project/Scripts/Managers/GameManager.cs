using _Project.Scripts.Behaviours;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Space(2.5F)]
        [SerializeField] private Camera mainCamera;
        
        [Header("Prefabs references")] 
        [SerializeField] private Asteroid asteroidPrefab;
        [SerializeField] private Ship shipPrefab;

        private int _currentWave = 1;
        
        private int _asteroidsInWave = 3;
        private int _waveIncrement = 2;
        
        private Ship _ship;

        private void Start()
        {
            CreatePlayer();
            StartNewWave();
        }

        private void CreatePlayer()
        {
            Ship playerShip = Instantiate(shipPrefab, Vector2.zero, Quaternion.identity);
            _ship = playerShip;
        }

        private void StartNewWave()
        {
            if (_currentWave != 1)
                _asteroidsInWave += _waveIncrement;
            
            for (int i = 0; i < _asteroidsInWave; i++)
            {
                Asteroid asteroid = Instantiate(asteroidPrefab, RandomPositionInScreen(), Quaternion.identity);
            }
        }

        private Vector3 RandomPositionInScreen()
        {
            float posX = Random.Range(0F, mainCamera.pixelWidth);
            float posY = Random.Range(0F, mainCamera.pixelHeight);
            float posZ = Random.Range(mainCamera.nearClipPlane, mainCamera.farClipPlane);

            return mainCamera.ScreenToWorldPoint(new Vector3(posX, posY, posZ));
        }
    }
}
