using UnityEngine;

public class QuadTree : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MakeTree(transform,512);
    }

    void MakeTree(Transform current, int size)
    {
        if (size == 4)
            return;
        for(byte i = 0; i < 4; i++)
        {
            GameObject child = new GameObject("child");
            child.transform.SetParent(current);
            child.transform.localPosition = new Vector3(size / 4 * ((i%2*2)-1), size / 4 * ((i/2*2) - 1));
            MakeTree(child.transform, size / 2);
        }
    }
}
