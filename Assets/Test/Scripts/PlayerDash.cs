using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private InputActionReference dashAction;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private PlayerMover mover;
    [SerializeField] private Health health;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float cooldown = 0.6f;

    private CharacterController controller;
    private bool isDashing;
    private float nextDashTime;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() => dashAction.action.Enable();
    private void OnDisable() => dashAction.action.Disable();

    private void Update()
    {
        if (isDashing || Time.time < nextDashTime) return;

        if (dashAction.action.WasPressedThisFrame())
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        // Pick a direction: where we're pushing, or where we're facing
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = input.sqrMagnitude > 0.01f
            ? new Vector3(input.x, 0f, input.y).normalized
            : transform.forward;

        // Start the dash
        isDashing = true;
        mover.enabled = false;
        health.IsInvulnerable = true;

        float speed = dashDistance / dashDuration;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            controller.Move(direction * speed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;   // wait one frame, then keep going
        }

        // End the dash
        mover.enabled = true;
        health.IsInvulnerable = false;
        isDashing = false;
        nextDashTime = Time.time + cooldown;
    }
}