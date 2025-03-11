using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class ItemObject : MonoBehaviour
{
    [ReadOnly]
    public ItemData data;

    private void OnValidate()
    {
        InIt();
    }

    private void InIt()
    {
        if (data == null) data = Resources.Load<ItemData>($"ItemData/{gameObject.name}");
    }

    private void Awake()
    {
        InIt();
    }
}
