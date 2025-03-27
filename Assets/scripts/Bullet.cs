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
        if (tree.size < Mathf.Abs(transform.position.x) || tree.size < Mathf.Abs(transform.position.y))
            Destroy(gameObject);
        GameObject zombie = tree.NearestNeighbor(transform, 1);
        if (zombie != null)
        {
            tree.Remove(zombie.transform);
            Destroy(zombie);
            Destroy(gameObject);
        }
    }
}
