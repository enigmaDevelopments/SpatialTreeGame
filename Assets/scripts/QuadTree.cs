using System.Collections.Generic;
using UnityEngine;

public class QuadTree : MonoBehaviour
{
    public void Insert(Transform zombie)
    {
        Transform treeLocation = GetLocation(zombie);
        zombie.SetParent(treeLocation);

            Split(treeLocation);
    }
    public void Move(Transform zombie)
    {
        if (Mathf.Abs(zombie.localPosition.x) <= .5f && Mathf.Abs(zombie.localPosition.y) <= .5f)
            return;
        Remove(zombie);
        Insert(zombie);
    }
    public void Remove(Transform zombie)
    {
        Transform grandparent = zombie.parent.parent;
        zombie.SetParent(null);
        merge(grandparent);
    }
    private void merge(Transform toMerge)
    {
        int neice = 0;
        foreach (Transform child in toMerge)
        {
            neice += child.childCount;
            if (2 < neice)
                return;
        }
        List<Transform> grandchilden = new List<Transform>();
        foreach (Transform child in toMerge)
        { 
            foreach (Transform grandchild in child)
                grandchilden.Add(grandchild);
            child.DetachChildren();
            Destroy(child.gameObject);
        }
        foreach (Transform grandchild in grandchilden)
            grandchild.SetParent(toMerge);
        merge(toMerge.parent);
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
        if (current.childCount != 3)
            return;
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
        foreach (Transform child in current)
            Split(child);
    }
}
