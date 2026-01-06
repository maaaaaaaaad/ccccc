using UnityEngine;

namespace UI.Common
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private Vector2Int _lastScreenSize;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            ApplySafeArea();
        }

        private void Update()
        {
            if (HasSafeAreaChanged())
            {
                ApplySafeArea();
            }
        }

        private bool HasSafeAreaChanged()
        {
            var currentSafeArea = Screen.safeArea;
            var currentScreenSize = new Vector2Int(Screen.width, Screen.height);

            if (currentSafeArea != _lastSafeArea)
            {
                return true;
            }

            if (currentScreenSize != _lastScreenSize)
            {
                return true;
            }

            return false;
        }

        private void ApplySafeArea()
        {
            var safeArea = Screen.safeArea;
            _lastSafeArea = safeArea;
            _lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}
