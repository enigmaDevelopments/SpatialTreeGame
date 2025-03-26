using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = .15f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.linearVelocity = (Vector2)transform.up * speed;
    }
    void Update()
    {
        if (64 < Mathf.Abs(transform.position.x) || 64 < Mathf.Abs(transform.position.y))
            Destroy(gameObject);
    }
}
