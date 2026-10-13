using System;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float turnSpeed = 720f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] float acceleration = 50f;
    [SerializeField] float deceleration = 60f;
    private Vector3 currentVelocity;
    


    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
    }
      

    // Update is called once per frame
    void Update()
    {

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        bool isSprinting = sprintAction.action.IsPressed();
        Vector3 direction = new Vector3(input.x, 0f, input.y);

        float speed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        Vector3 horizontal = direction * speed;

        bool hasInput = direction.sqrMagnitude > 0.01f;
        float rate = hasInput ? acceleration : deceleration;
        currentVelocity = Vector3.MoveTowards(currentVelocity, horizontal, rate * Time.deltaTime);

        //Gravity
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = currentVelocity + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime) ;

        if (hasInput)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
           transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation ,turnSpeed*Time.deltaTime);
        }
      
        
    }
}
