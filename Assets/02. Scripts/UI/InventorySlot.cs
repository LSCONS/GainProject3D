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

    private void OnValidate()
    {
        _text = transform.Find("StackCount").GetComponent<TextMeshProUGUI>();
        if (_text == null) Debug.LogError("_text is null");

        _icon = transform.Find("Icon").GetComponent<Image>();
        if (_text == null) Debug.LogError("_icon is null");

        _objectPool = transform.Find("ObjectPool").gameObject;
        if (_objectPool == null) Debug.LogError("_objectPool is null");
    }

    private void Awake()
    {
        UpdateAmountText();
        UpdateIcon();
    }

    //�ش� �������� �� �� �ִ� ������ĭ�� �ִ��� Ȯ���ϰ� ����ִ� �޼���
    public bool CheckInputItem(ItemObject inputItemObject)
    {
        if (_itemObject == null || 
            (_itemObject.data.canStack &&
            _itemObject.data.IDItem == inputItemObject.data.IDItem&&
            _itemAmount < _itemObject.data.maxStackAmount))
        {
            OnInputItem(inputItemObject);
            return true;
        }
        return false;
    }


    //�ش� �������� �ش� ĭ�� ����ִ� �޼���
    private void OnInputItem(ItemObject inputItemObject)
    {
        if(_itemObject == null)
        {
            _itemObject = inputItemObject;
            _icon.sprite = _itemObject.data.icon;
            UpdateIcon();
        }

        if (_itemObject.data.canStack)
        {
            _itemAmount++;
            UpdateAmountText();
        }

        inputItemObject.transform.SetParent(_objectPool.transform);
    }

    
    //�������� �ؽ�Ʈ�� �ʱ�ȭ�ϴ� �޼���
    private void UpdateAmountText()
    {
        if(_itemAmount == 0)
        {
            _text.gameObject.SetActive(false);
        }
        else
        {
            _text.text = _itemAmount.ToString();
            _text.gameObject.SetActive(true);
        }
    }


    //�������� �������� �ʱ�ȭ�ϴ� �޼���
    private void UpdateIcon()
    {
        if(_itemObject == null)
        {
            _icon.gameObject.SetActive(false);
        }
        else
        {
            _icon.gameObject.SetActive(true);
        }
    }
}
