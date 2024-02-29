using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    [Header("基础参数")] public float speed;

    public float jumpForce; // 跳跃力度

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
    }

    /// <summary>
    /// 被启动时
    /// </summary>
    private void OnEnable()
    {
        inputControl.Enable();
    }

    /// <summary>
    /// 更新时
    /// </summary>
    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// 固定更新的值，一般处理跟物理有关的数据
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 人物移动
    /// </summary>
    public void Move()
    {
        // 人物左右移动
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        // 人物翻转
        int faceDirection = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDirection = 1;
        if (inputDirection.x < 0)
            faceDirection = -1;
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }

    /// <summary>
    /// 跳
    /// </summary>
    /// <param name="obj"></param>
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 被关闭时
    /// </summary>
    private void OnDisable()
    {
        inputControl.Disable();
    }
}
