using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private const float MOVE_SPEED = 5f;
    [SerializeField] private const float JUMP_HEIGHT = 10f;

    [SerializeField] private float gravity = -9.81f;

    private Transform cameraTransform;
    private CharacterController characterController;

    private float moveSpeed = MOVE_SPEED;
    private float jumpHeight = JUMP_HEIGHT;

    public float slideFriction = 0.2f; // ajusting the friction of the slope

    private bool isGrounded; // is on ground

    private Vector3 velocity;
    private Vector3 hitNormal; //orientation of the slope.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions() {
        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 5f)) {
        //    Debug.Log(hit.normal);
        //    Draggable draggable = hit.collider.GetComponent<Draggable>();
        //    if (draggable != null) {
        //        if (InputManager.Instance.IsDragPressed()) {
        //            draggable.setIsDragged(true);
        //        } else {
        //            draggable.setIsDragged(false);
        //        }
        //    }
        //}
    }

    private void HandleMovement() {
        Vector2 movementInput = InputManager.Instance.GetMovement();
        if (characterController.isGrounded) {
            velocity.y = -1f;
            if (InputManager.Instance.IsJumpPressed()) {
                velocity.y = jumpHeight;
            }
        } else {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        isGrounded = Vector3.Angle(Vector3.up, hitNormal) <= characterController.slopeLimit;
        Vector3 moveDir = transform.TransformDirection(movementInput.x * cameraTransform.right + movementInput.y * cameraTransform.forward) * moveSpeed;
        if (!isGrounded) {
            moveDir.x += (1f - hitNormal.y) * hitNormal.x * (moveSpeed - slideFriction);
            moveDir.y += (1f - hitNormal.y) * hitNormal.z * (moveSpeed - slideFriction);
        }

        characterController.Move(moveDir * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
}