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
        if (QuadTree.size < Mathf.Abs(transform.position.x) || QuadTree.size < Mathf.Abs(transform.position.y))
            Destroy(gameObject);
        GameObject zombie = QuadTree.NearestNeighbor(transform, 1);
        if (zombie != null)
        {
            QuadTree.Remove(zombie.transform);
            Destroy(zombie);
            Destroy(gameObject);
        }
    }
}
