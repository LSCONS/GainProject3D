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
        data = Resources.Load<ItemData>($"ItemData/{gameObject.name}");
    }
}
