using TMPro;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class GameHUD : MonoBehaviour
    {
        [Header("Game Canvas Groups References")]
        [SerializeField] private CanvasGroup playGameCanvasGroup;
        [SerializeField] private CanvasGroup gameOverCanvasGroup;
        [SerializeField] private CanvasGroup gameplayHUDCanvasGroup;

        [Space(2.5F)][Header("UI References")] 
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI totalPoints;
        [SerializeField] private Transform livesTransform;

        [Space(5F)] 
        [SerializeField] private GameObject lifePrefab;

        private int _score;

        public void InitializeGameHUD(int numberOfLives)
        {
            playGameCanvasGroup.interactable = false;
            playGameCanvasGroup.alpha = 0F;
            gameplayHUDCanvasGroup.alpha = 1F;
            
            pointsText.text = _score.ToString();

            for (int i = 0; i < numberOfLives; i++)
            {
                Instantiate(lifePrefab, livesTransform);
            }
        }
        
        public void UpdatePoints(int increment)
        {
            _score += increment;
            pointsText.text = _score.ToString();
        }

        public void RemoveLife()
        {
            int childCount = livesTransform.childCount;

            Destroy(livesTransform.GetChild(childCount - 1).gameObject);
        }

        public void EnableGameOverPanel()
        {
            totalPoints.text = _score.ToString();
            
            gameOverCanvasGroup.interactable = true;
            gameOverCanvasGroup.alpha = 1F;
        }
    }
}