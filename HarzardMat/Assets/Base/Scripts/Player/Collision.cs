using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    [Space]
    public bool onGround, onWall, onRightWall, onLeftWall, onCeiling;
    public int wallSide;

    [Space]
    [Header("Collision")]
    public float collisionRadius = 0.23f;

    private CapsuleCollider2D capsuleCollider2D;
    public Vector2 bottomOffset,ceilingOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    private Vector2 originalOffSetRight, originalOffSetLeft, originalCapsuleOffSet, originalOffSetCeiling, originalOffSetBottom;
    PlayerLookDir lookDir;

    private void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        originalCapsuleOffSet = capsuleCollider2D.offset;
        originalOffSetRight = rightOffset;
        originalOffSetLeft = leftOffset;
        originalOffSetCeiling = ceilingOffset;
        originalOffSetBottom = bottomOffset;
    }

    private void Update()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onCeiling = Physics2D.OverlapCircle((Vector2)transform.position + ceilingOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
        Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        wallSide = onRightWall ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset, ceilingOffset, rightOffset, leftOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ceilingOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

    }

    public void LookSide(int side)
    {
        if(side == 1 && lookDir == PlayerLookDir.Left)
        {
            lookDir = PlayerLookDir.Right;
            CheckOffsets();
        }
        
        if(side == -1 && lookDir == PlayerLookDir.Right)
        {
            lookDir = PlayerLookDir.Left;
            CheckOffsets();
        }
    }

    public void CheckOffsets()
    {
        switch (lookDir)
        {
            case PlayerLookDir.Right:
                if (rightOffset != originalOffSetRight)
                {
                    rightOffset = originalOffSetRight;
                }

                if (leftOffset != originalOffSetLeft)
                {
                    leftOffset = originalOffSetLeft;
                }

                if (capsuleCollider2D.offset.x != originalCapsuleOffSet.x)
                {
                    capsuleCollider2D.offset = originalCapsuleOffSet;
                }
                if (bottomOffset != originalOffSetBottom)
                {
                    bottomOffset = originalOffSetBottom;
                }

                if (ceilingOffset != originalOffSetCeiling)
                {
                    ceilingOffset = originalOffSetCeiling;
                }

                break;
            case PlayerLookDir.Left:
                if (rightOffset.x != (-1 * originalOffSetRight.x))
                {
                    rightOffset.x = -1 * originalOffSetRight.x;
                }

                if (leftOffset.x != (-1 * originalOffSetLeft.x))
                {
                    leftOffset.x = -1 * originalOffSetLeft.x;
                }

                if (capsuleCollider2D.offset.x != (-1 * originalCapsuleOffSet.x))
                {
                    capsuleCollider2D.offset = new Vector2(-1 * originalCapsuleOffSet.x, originalCapsuleOffSet.y);
                }

                if (bottomOffset.x != (-1 * originalOffSetBottom.x))
                {
                    bottomOffset.x = -1 * originalOffSetBottom.x;
                }

                if (ceilingOffset.x != (-1 * ceilingOffset.x))
                {
                    ceilingOffset.x = -1 * originalOffSetCeiling.x;
                }
                break;
        }
    }

    public bool IsEverythingColliding()
    {
        if(onGround && onRightWall && onLeftWall && onCeiling)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
