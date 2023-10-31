using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class RawImageScroller : MonoBehaviour
    {
        [SerializeField]
        private RawImage _rawImage;

        [SerializeField]
        private float _xSpeed;

        [SerializeField]
        private float _ySpeed;

        private void Update()
        {
            _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_xSpeed, _ySpeed) * Time.deltaTime,
                _rawImage.uvRect.size);
        }
    }
}