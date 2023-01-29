using _Project.Scripts.Behaviours;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private const int AsteroidsInitialCount = 3;
        private const int WaveIncrement = 2;
        
        [Space(2.5F)]
        [SerializeField] private Camera mainCamera;
        
        [Header("Prefabs references")] 
        [SerializeField] private Asteroid asteroidPrefab;
        [SerializeField] private Ship shipPrefab;

        private int _currentWave = 1;
        public int _currentAsteroidsInWave;

        private Ship _ship;

        #region Event Register

        private void Awake()
        {
            EventManager.OnAsteroidDestroyed += UpdateAsteroidsCount;
            EventManager.OnAsteroidSplit += UpdateAsteroidsCount;
        }

        private void OnDestroy()
        {
            EventManager.OnAsteroidDestroyed -= UpdateAsteroidsCount;
            EventManager.OnAsteroidSplit -= UpdateAsteroidsCount;
        }

        #endregion
        
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
            _currentAsteroidsInWave = AsteroidsInitialCount;
            
            if (_currentWave != 1) _currentAsteroidsInWave += WaveIncrement;

            for (int i = 0; i < _currentAsteroidsInWave; i++)
                Instantiate(asteroidPrefab, RandomPositionInScreen(), Quaternion.identity);
        }

        private Vector3 RandomPositionInScreen()
        {
            float posX = Random.Range(0F, mainCamera.pixelWidth);
            float posY = Random.Range(0F, mainCamera.pixelHeight);
            float posZ = Random.Range(mainCamera.nearClipPlane, mainCamera.farClipPlane);

            return mainCamera.ScreenToWorldPoint(new Vector3(posX, posY, posZ));
        }

        private void UpdateAsteroidsCount(int increment)
        {
            _currentAsteroidsInWave += increment;

            if (_currentAsteroidsInWave != 0) return;
            
            _currentWave++;
            
            StartNewWave();
        }
    }
}
