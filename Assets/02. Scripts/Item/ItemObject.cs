using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public interface IInteractalbe
{
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractalbe
{
    [ReadOnly]
    public ItemData data;

    private void OnValidate()
    {
        data = Resources.Load<ItemData>($"ItemData/{gameObject.name}");
    }

    public void OnInteract()
    {

    }
}
