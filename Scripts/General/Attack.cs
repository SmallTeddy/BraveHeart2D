using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage; // 攻击伤害
    public float attackRange; // 攻击范围
    public float attackRate; // 攻击频率

    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
