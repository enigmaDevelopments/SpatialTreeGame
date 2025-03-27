using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = .15f;
    public Rigidbody2D rb;
    public QuadTree tree;
    void Start()
    {
        rb.linearVelocity = (Vector2)transform.up * speed;
    }
    void Update()
    {
        if (64 < Mathf.Abs(transform.position.x) || 64 < Mathf.Abs(transform.position.y))
            Destroy(gameObject);
        if (tree.PointInRadius(transform, 1))
        {
            GameObject zombie = tree.NearestNeighbor(transform,1);
            tree.Remove(zombie.transform);
            Destroy(zombie);
            Destroy(gameObject);
        }
    }
}
