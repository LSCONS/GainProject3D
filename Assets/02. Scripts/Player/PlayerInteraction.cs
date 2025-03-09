using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class PlayerInteraction : MonoBehaviour
{

    [ShowInInspector, ReadOnly]
    private GameObject currentObject;
    private float _DistanceMax = 5f;
    private Camera _camera;
    private LayerMask _LayerMask;
    

    private void OnValidate()
    {
        _camera = Camera.main;
        _LayerMask = ReadonlyData.interactionLayerMask;
    }

    private void Update()
    {
        ShootingLayCastForCamera();
    }

    private void ShootingLayCastForCamera()
    {
        Ray _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, _DistanceMax, _LayerMask))
        {
            if(_hit.collider.gameObject != currentObject)
            {
                currentObject = _hit.collider.gameObject;
            }
        }
        else
        {
            currentObject = null;
        }
    }
}
