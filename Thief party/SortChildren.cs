using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SortChildren : MonoBehaviour
{
    public void SortChildrenByName()
	{
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        children = children.OrderBy(o => o.name).ToList();

        foreach (Transform child in children)
        {
            child.SetParent(null);
        }

        foreach (Transform child in children)
        {
            child.SetParent(transform, true);
        }

        for (int i = transform.childCount - 1; i > -1; i--)
        {
            transform.GetChild(i).SetAsLastSibling();
        }
    }
}
