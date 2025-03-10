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


    //Enter�� �ƴ� Stay�� ����� ����
    //�ε����� collision�� ��ġ�� �������� ��ǩ������ ����� �Ǵܵ� ��� ������ ��Ŵ.
    //���� �ش� �������� ������ �ε����鼭 Exit�� ���� ���ϰ� �����븦 �ö�� ���
    //�ε��� �� ���� ȣ��Ǵ� Enter������ ����� �۵����� ���� ���ɼ��� ����Ǳ� ������ Stay�� �ۼ���.
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



    //�浹�� ó���� Stay���� �ϰ� �ֱ� ������ ���� ó���� �ϴ� �޼��尡 ���� �� ȣ��Ǵ� ����� �־� �ڷ�ƾ���� ó��.
    private IEnumerator jumpCoolTime(PlayerControl playerControl)
    {
        playerControl.OnJumpPlatform(jumpForce);
        yield return new WaitForSeconds(0.125f);
        jumpCoroutine = null;
    }
}
