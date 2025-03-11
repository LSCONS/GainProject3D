using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class InventorySlotGrid : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private InventorySlot[] inventorySlots;
    public int selectItemSlotIndex = -1;

    private void OnValidate()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].slotIndex = i;
        }
    }

    private void OnEnable()
    {
        selectItemSlotIndex = -1;
        UIManager.Instance.SetAcitveButton(false);
    }


    public void CheckItemSlot(ItemObject itemObject)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].CheckInputItem(itemObject))
            {
                itemObject.gameObject.SetActive(false);
                return;
            }
        }

        //TODO: 넣을 수 있는 아이템 칸을 찾을 수 없는 경우의 명령어 필요
    }


    public void SelectedItemSlot(int index)
    {
        UIManager.Instance.SetAcitveButton(true);
        selectItemSlotIndex = index;
    }


    public void UseItem()
    {
        if(selectItemSlotIndex != -1)
        {
            inventorySlots[selectItemSlotIndex].UseItem();
        }
    }


    public void ThrowItem(int index)
    {

    }
}
