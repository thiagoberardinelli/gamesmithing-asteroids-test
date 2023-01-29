using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class GameHUD : MonoBehaviour
    {
        [Header("UI References")] 
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private Transform livesTransform;

        [Space(2.5F)] 
        [SerializeField] private GameObject lifePrefab;

        private int _points;

        public void Initialize(int numberOfLives)
        {
            pointsText.text = _points.ToString();

            for (int i = 0; i < numberOfLives; i++)
            {
                Instantiate(lifePrefab, livesTransform);
            }
        }
        
        public void UpdatePoints(int increment)
        {
            _points += increment;
            pointsText.text = _points.ToString();
        }

        public void RemoveLife()
        {
            int childCount = livesTransform.childCount;

            Destroy(livesTransform.GetChild(childCount - 1).gameObject);
        }
    }
}