using UnityEngine;

public class TreeInterface : MonoBehaviour
{
    public QuadTree tree;
    void Update()
    {
        tree.Move(transform);
    }
}
