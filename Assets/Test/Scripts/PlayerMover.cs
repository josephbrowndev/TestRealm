using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float turnSpeed = 720f;


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
        transform.position += direction * speed * Time.deltaTime;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
           transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation ,turnSpeed*Time.deltaTime);
        }
      
        
    }
}
