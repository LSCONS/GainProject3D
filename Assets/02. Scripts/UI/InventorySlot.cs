using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VInspector;
using static UnityEngine.UI.CanvasScaler;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;
    [ShowInInspector, ReadOnly]
    private Image _icon;
    private TextMeshProUGUI _text;
    public ItemObject _itemObject;
    private int _itemAmount = 0;
    private GameObject _objectPool;
    private Button _button;
    [ShowInInspector, ReadOnly]
    private InventorySlotGrid _inventorySlotGrid;

    private void OnValidate()
    {
        InIt();
    }


    private void InIt()
    {
        if (_text == null) _text = transform.TransformFindAndGetComponent<TextMeshProUGUI>("StackCount");
        if (_icon == null) _icon = transform.TransformFindAndGetComponent<Image>("Icon");
        if (_objectPool == null) _objectPool = transform.TransformFindAndGetComponent<Transform>("ObjectPool").gameObject;
        if (_button == null) _button = transform.GetComponentDebug<Button>();
        if (_inventorySlotGrid == null) _inventorySlotGrid = GetComponentInParent<InventorySlotGrid>();
    }


    private void Awake()
    {
        InIt();
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


    //아이템 슬롯을 선택할 때 실행할 메서드
    private void SelectedSlot()
    {
        _inventorySlotGrid.SelectedItemSlot(slotIndex);
    }


    //아이템을 제거할 때 실행할 메서드
    public void ReduceItem()
    {
        if (_itemObject.data.canStack)
        {
            _itemAmount--;
            UpdateAmountText();
        }

        if (_itemAmount == 0)
        {
            _itemObject = null;
            UpdateIcon();
            _button.onClick.RemoveListener(SelectedSlot);
        }
    }
}
