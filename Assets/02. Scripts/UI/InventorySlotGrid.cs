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
        InIt();
    }

    private void InIt()
    {
        if (inventorySlots == null)
        {
            inventorySlots = GetComponentsInChildren<InventorySlot>();
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].slotIndex = i;
            }
        }
    }

    private void Awake()
    {
        InIt();
    }

    private void OnEnable()
    {
        selectItemSlotIndex = -1;
        UIManager.Instance.SetAcitveButton(false);
    }


    //넣을 수 있는 아이템 칸이 있는지 확인하고 아이템을 집어넣는 메서드
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

        //TODO: 아이템 칸이 꽉 차서 아이템 칸을 찾을 수 없는 경우의 예외처리 명령어 필요 
    }



    //아이템이 선택된 경우 상호작용이 가능한 버튼을 활성화하고 해당 아이템 슬롯의 번호를 지정하는 메서드
    public void SelectedItemSlot(int index)
    {
        UIManager.Instance.SetAcitveButton(true);
        selectItemSlotIndex = index;
    }


    //현재 선택된 아이템 칸의 아이템을 사용하는 메서드
    public void UseItem()
    {
        if(selectItemSlotIndex != -1)
        {
            inventorySlots[selectItemSlotIndex].UseItem();
        }
    }


    //TODO: 아이템 던지는 메서드 추가 예정
    public void ThrowItem(int index)
    {

    }
}
