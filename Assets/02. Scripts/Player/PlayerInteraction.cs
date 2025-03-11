using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class PlayerInteraction : MonoBehaviour
{

    [ShowInInspector, ReadOnly]
    private GameObject currentObject;
    private float _DistanceMax = 5f;
    private Camera _camera;
    private LayerMask _LayerMask;
    [ShowInInspector, ReadOnly]
    private InteractionUI _interactionUI;
    [ShowInInspector, ReadOnly]
    private InventorySlotGrid _inventorySlotGrid;
    [ShowInInspector, ReadOnly]
    private PlayerInput _playerInput;
    private ItemObject _itemObject;


    private void OnValidate()
    {
        _camera = Camera.main;
        _LayerMask = ReadonlyData.interactionLayerMask;
        _interactionUI = FindFirstObjectByType<InteractionUI>();
        if (_interactionUI == null) Debug.LogError("_interactionUI is null");
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null) Debug.LogError("_palyerInput is null");
        _inventorySlotGrid = FindFirstObjectByType<InventorySlotGrid>();
        if (_inventorySlotGrid == null) Debug.LogError("_inventorySlotGrid is null");
    }

    private void Update()
    {
        ShootingLayCastForCamera();
    }


    //카메라 가운데로 레이케스트를 쏴서 상호작용 가능한 오브젝트를 확인하는 메서드 
    private void ShootingLayCastForCamera()
    {
        Ray _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, _DistanceMax, _LayerMask))
        {
            if(_hit.collider.gameObject != currentObject)
            {
                currentObject = _hit.collider.gameObject;
                _itemObject = currentObject.GetComponentInParent<ItemObject>();
                if (_itemObject == null) Debug.LogError("itemObject is null");
                _playerInput.interactionAction -= InteractionHandler;
                _playerInput.interactionAction += InteractionHandler;
                UIManager.Instance.UpdateInteractionUI(_itemObject);
            }
        }
        else
        {
            if(_itemObject != null)
            {
                _playerInput.interactionAction -= InteractionHandler;
                _itemObject = null;
            }

            if(currentObject != null)
            {
                currentObject = null;
                UIManager.Instance.UpdateInteractionUI(null);
            }
        }
    }

    private void InteractionHandler()
    {
        _inventorySlotGrid.CheckItemSlot(_itemObject);
    }
}
