using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    public float checkRadius; // 检测半径
    public bool isGround; // 是否在地面
    private void Update()
    {
        Check();
    }

    /// <summary>
    /// 检测
    /// </summary>
    public void Check()
    {
        // 检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }

    /// <summary>
    /// 在场景视图中绘制检测半径
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
