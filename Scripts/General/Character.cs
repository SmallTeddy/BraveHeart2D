using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth; // 最大生命值
    public float currentHealth; // 当前生命值

    [Header("受伤无敌")]
    public float invulnerableDuration; // 无敌时间
    private float invulnerableCounter; // 无敌计时器
    public bool invulnerable; // 是否处于无敌状态

    [Header("事件")]
    public UnityEvent<Transform> onTakeDamage; // 受伤事件
    public UnityEvent onDie; // 死亡事件


    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="attacker"></param>
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
            return;

        if (currentHealth - attacker.damage > 0)
        {
            // 减少生命值
            currentHealth -= attacker.damage;
            // 触发无敌
            TriggerInvulnerable();
            // 执行受伤
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            // 触发死亡
            onDie?.Invoke();
        }
    }

    /// <summary>
    /// 触发无敌
    /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
