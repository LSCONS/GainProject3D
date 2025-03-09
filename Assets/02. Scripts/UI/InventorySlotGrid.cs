using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class InventorySlotGrid : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private InventorySlot[] inventorySlots;

    private void OnValidate()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].slotIndex = i;
        }
    }
}
