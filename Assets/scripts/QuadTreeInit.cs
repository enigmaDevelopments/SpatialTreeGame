using UnityEngine;

public class QuadTreeInit : MonoBehaviour
{
    public float size;
    void Start()
    {
        transform.localScale = new Vector3(size * 2, size * 2, 1);
        QuadTree.transform = transform;
        QuadTree.size = size;
    }
}
