using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Laser : MonoBehaviour
{
    public Sprite on, off;

    private new SpriteRenderer renderer;
    private new BoxCollider2D collider;

    public void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Toggle()
    {
        collider.enabled = !collider.enabled;
        if (collider.enabled)
        {
            renderer.sprite = on;
        }
        else
        {
            renderer.sprite = off;
        }
    }
}