using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private InteractionUI _interactionUI;
    [ReadOnly]
    public GameObject interactionUIObject;
    [ReadOnly]
    public GameObject inventoryUIObject;
    [ReadOnly]
    public StatusUI statusUI;
    [ShowInInspector, ReadOnly]
    public Button _upButton;
    [ShowInInspector, ReadOnly]
    public Button _downButton;
    [ShowInInspector, ReadOnly]
    public PlayerStatus playerStatus;
    [ShowInInspector, ReadOnly]
    private InventorySlotGrid _inventorySlotGrid;

    private void OnValidate()
    {
        InIt();
    }

    private void InIt()
    {
        if (interactionUIObject == null) interactionUIObject = "InteractionUI".GetComponentNameDFS<Transform>().gameObject;
        if (inventoryUIObject == null) inventoryUIObject = "InventoryUI".GetComponentNameDFS<Transform>().gameObject;
        if (_interactionUI == null) _interactionUI = Util.FindFirstObjectByTypeDebug<InteractionUI>();
        if (statusUI == null) statusUI = Util.FindFirstObjectByTypeDebug<StatusUI>();
        if (_upButton == null) _upButton = "UpButton".GetComponentNameDFS<Button>();
        if (_downButton == null) _downButton = "DownButton".GetComponentNameDFS<Button>();
        if (_inventorySlotGrid == null) _inventorySlotGrid = Util.FindFirstObjectByTypeDebug<InventorySlotGrid>();
        if (playerStatus == null) playerStatus = Util.FindFirstObjectByTypeDebug<PlayerStatus>();
    }

    protected override void Awake()
    {
        base.Awake();
        InIt();
        _upButton.onClick.RemoveListener(_inventorySlotGrid.UseItem);
        _upButton.onClick.AddListener(_inventorySlotGrid.UseItem);
        interactionUIObject.SetActive(false);
        inventoryUIObject.SetActive(false);
        SetAcitveButton(false);
    }


    //상호 작용 UI를 업데이트하고 비활성화 및 활성화를 하는 메서드 
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

    
    //인벤토리를 활성화 및 비활성화하는 메서드
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


    //버튼을 활성화/비활성화 해주는 메서드
    public void SetAcitveButton(bool isActive)
    {
        _downButton.gameObject.SetActive(isActive);
        _upButton.gameObject.SetActive(isActive);
    }
}
