using System.Collections.Generic;
using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public void Insert(Transform zombie)
    {
        Transform treeLocation = GetLocation(zombie);
        zombie.SetParent(treeLocation);
        if (treeLocation.childCount == 3)
            Split(treeLocation);
    }
    public Transform GetLocation(Transform find)
    {
        return GetLocation(find, transform);
    }
    private Transform GetLocation(Transform find,Transform current)
    {
        if (current.childCount != 4)
            return current;
        int position = 0;
        if (current.position.x < find.position.x)
            position += 1;
        if (current.position.y < find.position.y)
            position += 2;
        return GetLocation(find, current.GetChild(position));
    }
    private void Split(Transform current)
    {
        float size = current.lossyScale.x;
        List<Transform> childen = new List<Transform>();
        foreach (Transform child in current)
            childen.Add(child);
        current.DetachChildren();
        for (byte i = 0; i < 4; i++)
        {
            GameObject child = new GameObject();
            Transform transChild = child.transform;
            transChild.SetParent(current);
            transChild.localPosition = new Vector3(.25f * ((i%2*2)-1), .25f * ((i/2*2) - 1));
            transChild.localScale = new Vector3(.5f, .5f, 1);
        }
        foreach (Transform child in childen)
            child.SetParent(GetLocation(child,current));
    }
}
