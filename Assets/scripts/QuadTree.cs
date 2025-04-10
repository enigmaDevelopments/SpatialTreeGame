using System.Collections.Generic;
using UnityEngine;

public static class QuadTree
{
    public static Transform root;
    public static float size = 64;
    public static void Insert(Transform zombie)
    {
        Transform treeLocation = GetLocation(zombie);
        zombie.SetParent(treeLocation);
        Split(treeLocation);
    }
    public static void Move(Transform zombie)
    {
        if (Mathf.Abs(zombie.localPosition.x) <= .5f && Mathf.Abs(zombie.localPosition.y) <= .5f)
            return;
        Remove(zombie);
        Insert(zombie);
    }
    public static void Remove(Transform zombie)
    {
        Transform grandparent = zombie.parent.parent;
        zombie.SetParent(null);
        merge(grandparent);
    }
    public static GameObject NearestNeighbor(Transform find)
    {
        return NearestNeighbor(find, size * Mathf.Sqrt(2));
    }
    public static GameObject NearestNeighbor(Transform find, float min)
    {
        GameObject output = null;
        NearestNeighbor(root, find, ref output, ref min);
        return output;
    }
    private static void NearestNeighbor(Transform current, Transform find, ref GameObject output, ref float min)
    {
        if (!Intersects(current, find, min))
            return;
        if (current.childCount == 4)
        {
            foreach (Transform child in current)
                NearestNeighbor(child, find, ref output, ref min);
            return;
        }
        foreach (Transform child in current)
        {
            float distance = Vector2.Distance(child.position, find.position);
            if (distance <= min)
            {
                min = distance;
                output = child.gameObject;
            }
        }
    }
    public static GameObject[] GetInRadus(Transform find, float radius)
    {
        List<GameObject> output = new List<GameObject>();
        GetInRadus(root, find, output, radius);
        return output.ToArray();
    }
    private static void GetInRadus(Transform current, Transform find, List<GameObject> output, float radius)
    {
        if (!Intersects(current, find, radius))
            return;
        if (current.childCount == 4)
        {
            foreach (Transform child in current)
                GetInRadus(child, find, output, radius);
            return;
        }
        foreach (Transform child in current)
            if (Vector2.Distance(child.position, find.position) <= radius)
                output.Add(child.gameObject);
    }
    public static bool PointInRadius(Transform check, float radius)
    {
        return PointInRadius(root, check, radius);
    }
    private static bool PointInRadius(Transform current, Transform check, float radius)
    {
        if (!Intersects(current, check, radius))
            return false;
        if (current.childCount == 4)
        {
            foreach (Transform child in current)
                if (PointInRadius(child, check, radius))
                    return true;
            return false;
        }
        foreach (Transform child in current)
            if (Vector2.Distance(child.position,check.position) <= radius)
                return true;
        return false;
    }
    public static Transform GetLocation(Transform find)
    {
        return GetLocation(find, root);
    }
    private static Transform GetLocation(Transform find,Transform current)
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
    private static bool Intersects(Transform node,Transform check, float radius)
    {
        float size = (node.lossyScale.x + radius)/2;
        return Mathf.Abs(check.position.x - node.position.x) <= size && Mathf.Abs(check.position.y - node.position.y) <= size;
    }
    private static void merge(Transform toMerge)
    {
        int grandchildCount = 0;
        foreach (Transform child in toMerge)
        {
            if (child.childCount == 4)
                merge(child);
            grandchildCount += child.childCount;
            if (2 < grandchildCount)
                return;
        }
        List<Transform> grandchilden = new List<Transform>();
        List<Transform> childeren = new List<Transform>();
        foreach (Transform child in toMerge)
        {
            foreach (Transform grandchild in child)
                grandchilden.Add(grandchild);
            childeren.Add(child);
        }
        foreach (Transform grandchild in grandchilden)
            grandchild.SetParent(toMerge);
        foreach (Transform child in childeren)
        {
            child.SetParent(null);
            MonoBehaviour.Destroy(child.gameObject,.001f);
        }
        merge(toMerge.parent);
    }
    private static void Split(Transform current)
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
