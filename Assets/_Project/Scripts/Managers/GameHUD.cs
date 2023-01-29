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
        [SerializeField] private List<GameObject> lives;

        private int _points;

        private void Start()
        {
            pointsText.text = _points.ToString();
        }
        
        public void UpdatePoints(int increment)
        {
            _points += increment;
            pointsText.text = _points.ToString();
        }

        public void RemoveLife() => Destroy(lives.Last().gameObject);
    }
}