using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Samples.Jetpack.Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Laser : MonoBehaviour
    {
        public Sprite on, off;

        private SpriteRenderer _renderer;
        private BoxCollider2D _collider;

        public void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Toggle()
        {
            _collider.enabled = !_collider.enabled;
            if (_collider.enabled)
            {
                _renderer.sprite = on;
            }
            else
            {
                _renderer.sprite = off;
            }
        }
    }
}