using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpPlatFormInteraction
{
    public void OnJumpPlatform(float jumpForce);
}

public class JumpingPlatform : MonoBehaviour
{
    private LayerMask _interactionLayer;
    private BoxCollider _boxCollider;
    private Coroutine jumpCoroutine;
    private float _positionY;
    public float CorrectionFactor = 0.3f;
    public float jumpForce = 10f;

    private void OnValidate()
    {
        _interactionLayer = 1 << LayerMask.NameToLayer("Player");
        _boxCollider = GetComponent<BoxCollider>();
        _positionY = transform.position.y + _boxCollider.center.y + (_boxCollider.size.y * 0.5f) - CorrectionFactor;
    }

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


    private IEnumerator jumpCoolTime(PlayerControl playerControl)
    {
        playerControl.OnJumpPlatform(jumpForce);
        yield return new WaitForSeconds(0.125f);
        jumpCoroutine = null;
    }
}
