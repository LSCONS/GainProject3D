using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;
    [ShowInInspector, ReadOnly]
    private Image _icon;
    private TextMeshProUGUI _text;
    private ItemObject _itemObject;
    private int _itemAmount = 0;
    private GameObject _objectPool;
    private Button _button;
    [ShowInInspector, ReadOnly]
    private InventorySlotGrid _inventorySlotGrid;

    private void OnValidate()
    {
        _text = transform.TransformFindAndGetComponent<TextMeshProUGUI>("StackCount");
        _icon = transform.TransformFindAndGetComponent<Image>("Icon");
        _objectPool = transform.TransformFindAndGetComponent<Transform>("ObjectPool").gameObject;
        _button = transform.GetComponentDebug<Button>();
        _inventorySlotGrid = GetComponentInParent<InventorySlotGrid>();
    }

    private void Awake()
    {
        UpdateAmountText();
        UpdateIcon();
    }


    //해당 아이템이 들어갈 수 있는 아이템칸이 있는지 확인하고 집어넣는 메서드
    public bool CheckInputItem(ItemObject inputItemObject)
    {
        if (_itemObject == null ||
            (_itemObject.data.canStack &&
            _itemObject.data.IDItem == inputItemObject.data.IDItem &&
            _itemAmount < _itemObject.data.maxStackAmount))
        {
            OnInputItem(inputItemObject);
            return true;
        }
        return false;
    }


    //해당 아이템을 해당 칸에 집어넣는 메서드
    private void OnInputItem(ItemObject inputItemObject)
    {
        if (_itemObject == null)
        {
            _itemObject = inputItemObject;
            _icon.sprite = _itemObject.data.icon;
            _button.onClick.AddListener(SelectedSlot);
            UpdateIcon();
        }

        if (_itemObject.data.canStack)
        {
            _itemAmount++;
            UpdateAmountText();
        }

        inputItemObject.transform.SetParent(_objectPool.transform);
    }


    //아이템의 텍스트를 초기화하는 메서드
    private void UpdateAmountText()
    {
        if (_itemAmount == 0)
        {
            _text.gameObject.SetActive(false);
        }
        else
        {
            _text.text = _itemAmount.ToString();
            _text.gameObject.SetActive(true);
        }
    }


    //아이템의 아이콘을 초기화하는 메서드
    private void UpdateIcon()
    {
        if (_itemObject == null)
        {
            _icon.gameObject.SetActive(false);
        }
        else
        {
            _icon.gameObject.SetActive(true);
        }
    }


    //아이템을 사용할 때 실행할 메서드  
    public void UseItem()
    {
        switch (_itemObject.data.type)
        {
            case ItemType.Health:
                UIManager.Instance.playerStatus.HealthChange(_itemObject.data.value);
                break;

            case ItemType.Stamina:
                UIManager.Instance.playerStatus.StartStaminaCoroutine(_itemObject.data.value, _itemObject.data.duration);
                break;
        }
        ReduceItem();
    }

    private void SelectedSlot() 
    {
        _inventorySlotGrid.SelectedItemSlot(slotIndex);
    }

    public void ReduceItem()
    {
        if (_itemObject.data.canStack)
        {
            _itemAmount--;
        }

        if(_itemAmount == 0)
        {
            _itemObject = null;
            UpdateIcon();
            UpdateAmountText();
            _button.onClick.RemoveListener(SelectedSlot);
        }
    }
}
