using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public GameObject tile;
    void Start()
    {
        MakeTree(transform,128);
    }

    void MakeTree(Transform current, int size)
    {
        if (size == 2)
            return;
        for(byte i = 0; i < 4; i++)
        {
            GameObject child;
            if (size == 4)
                child = Instantiate(tile);
            else
                child = new GameObject("child");
            child.transform.SetParent(current);
            child.transform.localPosition = new Vector3(size / 4 * ((i%2*2)-1), size / 4 * ((i/2*2) - 1));
            MakeTree(child.transform, size / 2);
        }
    }
}
