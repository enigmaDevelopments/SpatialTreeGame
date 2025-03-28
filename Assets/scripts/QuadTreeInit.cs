using UnityEngine;

public class QuadTreeInit : MonoBehaviour
{
    public float size;
    void Start()
    {
        transform.localScale = new Vector3(size * 2, size * 2, 1);
        QuadTree.root = transform;
        QuadTree.size = size;
    }
}
