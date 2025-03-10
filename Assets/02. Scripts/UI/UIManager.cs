using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class UIManager : Singleton<UIManager>
{
    [ReadOnly]
    public GameObject interactionUIObject;
    [ReadOnly]
    public GameObject inventoryUIObject;
    [ShowInInspector, ReadOnly]
    private InteractionUI _interactionUI;
    [ReadOnly]
    public StatusUI statusUI;

    private void OnValidate()
    {
        _interactionUI = FindFirstObjectByType<InteractionUI>();
        if (_interactionUI == null) Debug.LogError("interactionUI is null"); 

        interactionUIObject = transform.GetGameObjectSameNameDFS("InteractionUI").gameObject;
        if (interactionUIObject == null) Debug.LogError("interactionUIObject is null");

        inventoryUIObject = transform.GetGameObjectSameNameDFS("InventoryUI").gameObject;
        if (inventoryUIObject == null) Debug.LogError("inventoryUIObject is null");

        statusUI = FindFirstObjectByType<StatusUI>();
        if (statusUI == null) Debug.LogError("statusUI is null");
    }

    protected override void Awake()
    {
        base.Awake();
        interactionUIObject.SetActive(false);
        inventoryUIObject.SetActive(false);
    }


    //��ȣ �ۿ� UI�� ������Ʈ�ϰ� ��Ȱ��ȭ �� Ȱ��ȭ�� �ϴ� �޼���
    public void UpdateInteractionUI(ItemObject itemObject)
    {
        if(itemObject != null)
        {
            _interactionUI.OnLoadText(itemObject);
            interactionUIObject.SetActive(true);
        }
        else
        {
            interactionUIObject.SetActive(false);
        }
    }

    
    public void ActiveInventory(bool isActive)
    {
        if (isActive)
        {
            inventoryUIObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inventoryUIObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
