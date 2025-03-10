using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VInspector;

public interface IJumpPlatFormInteraction
{
    public void OnJumpPlatform(float jumpForce);
}

public class JumpingPlatform : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private LayerMask _interactionLayer;
    private BoxCollider _boxCollider;
    private Coroutine jumpCoroutine;
    private float _positionY;
    [ShowInInspector]
    private float CorrectionFactor = 0.3f;
    [ShowInInspector]
    private float jumpForce = 10f;

    private void OnValidate()
    {
        _interactionLayer = ReadonlyData.playerLayerMask;
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Awake()
    {
        _positionY = transform.position.y + _boxCollider.center.y + (_boxCollider.size.y * 0.5f) - CorrectionFactor;
    }


    //Enter가 아닌 Stay를 사용한 이유
    //부딪히는 collision의 위치가 점프대의 좌푯값보다 위라고 판단된 경우 점프를 시킴.
    //따라서 해당 점프대의 옆면을 부딪히면서 Exit을 받지 못하고 점프대를 올라온 경우
    //부딪힌 한 번만 호출되는 Enter에서는 제대로 작동하지 않을 가능성이 우려되기 때문에 Stay로 작성함.
    private void OnCollisionStay(Collision collision)
    {
        if(_interactionLayer == (_interactionLayer | 1 << collision.gameObject.layer) &&
            collision.transform.position.y >= _positionY &&
            collision.gameObject.TryGetComponent<PlayerControl>(out PlayerControl playerControl))
        {
            if(jumpCoroutine == null)
            {
                jumpCoroutine = StartCoroutine(jumpCoolTime(playerControl));
            }
        }
    }



    //충돌의 처리를 Stay에서 하고 있기 때문에 점프 처리를 하는 메서드가 여러 번 호출되는 우려가 있어 코루틴으로 처리.
    private IEnumerator jumpCoolTime(PlayerControl playerControl)
    {
        playerControl.OnJumpPlatform(jumpForce);
        yield return new WaitForSeconds(0.125f);
        jumpCoroutine = null;
    }
}
