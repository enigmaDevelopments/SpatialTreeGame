using UnityEngine;

public class TreeInterface : MonoBehaviour
{
    public QuadTree tree;
    void Update()
    {
        tree.Move(transform);
    }
    void OnDestroy()
    {
        Debug.LogException(new System.Exception("ouchie :3"));
    }
}
