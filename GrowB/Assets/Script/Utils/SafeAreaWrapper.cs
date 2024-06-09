using UnityEngine;

namespace Utils
{
    public class SafeAreaWrapper : MonoBehaviour
    {
        Vector2 _minAnchor;
        Vector2 _maxAnchor;

        private void Start()
        {
            var myrect = this.GetComponent<RectTransform>();

            _minAnchor = Screen.safeArea.min;
            _maxAnchor = Screen.safeArea.max;

            _minAnchor.x /= Screen.width;
            _minAnchor.y /= Screen.height;

            _maxAnchor.x /= Screen.width;
            _maxAnchor.y /= Screen.height;
        

            myrect.anchorMin = _minAnchor;
            myrect.anchorMax = _maxAnchor;

        }
    }
}
