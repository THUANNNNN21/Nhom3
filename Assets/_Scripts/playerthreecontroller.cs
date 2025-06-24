using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] //tu dong them rigid2d neu chua gan thu cong

public class playerthreecontroller : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    Vector2 moveInput;
    public bool isMoving {  get; private set; }
    public Rigidbody2D rigid2d;

    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 move = (Vector2)rigid2d.position + moveInput * walkSpeed * Time.deltaTime;
        rigid2d.MovePosition(move);
    }

    public void OnMove (InputAction.CallbackContext context)
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero; //vector2.zero la (0,0) tuc la khong di chuyen; neu true thi dang di chuyen, false la khong di chuyen; 
    }
}
