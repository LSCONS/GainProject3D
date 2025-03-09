using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;
    [ShowInInspector, ReadOnly]
    private Sprite icon;
}
