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

    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
    }

    // 被启动时
    private void OnEnable()
    {
        inputControl.Enable();
    }

    // 更新时
    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    // 固定更新的值，一般处理跟物理有关的数据
    private void FixedUpdate()
    {
        Move();
    }

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

    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGround)
         rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    // 被关闭时
    private void OnDisable()
    {
        inputControl.Disable();
    }
}