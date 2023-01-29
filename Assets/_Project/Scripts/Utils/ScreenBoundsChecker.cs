using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class ScreenBoundsChecker : MonoBehaviour
    {
        private Camera _camera;
        
        private void Start() => _camera = Camera.main;

        private void Update() => CheckScreenBounds();

        private void CheckScreenBounds()
        {
            if (Camera.main == null) return;
            
            Vector3 viewPortPoint = _camera.WorldToViewportPoint(transform.position);
        
            if (viewPortPoint.x < 0)
                viewPortPoint = new Vector3(1F,  viewPortPoint.y, viewPortPoint.z);
            else if (viewPortPoint.x >= 1.05)
                viewPortPoint = new Vector3(0F, viewPortPoint.y, viewPortPoint.z);
            
            if (viewPortPoint.y < 0)
                viewPortPoint = new Vector3(viewPortPoint.x,  1F, viewPortPoint.z);
            else if (viewPortPoint.y >= 1.05F)
                viewPortPoint = new Vector3(viewPortPoint.x, 0F, viewPortPoint.z);
            
            transform.position = _camera.ViewportToWorldPoint(viewPortPoint);
        }
    }
}