
using System;
using UnityEngine;

public class Draggable : MonoBehaviour {
    private Vector3 offsetFromCamera;
    private Vector3 targetPosition;
    [SerializeField] private float dragSpeed = 5f;

    public void Start() {
        offsetFromCamera = Vector3.zero;
        targetPosition = transform.position;
    }

    public void Update() {
        if (InputManager.Instance.IsDragPressed()) {
            // Get the position of the center of the screen in the world
            Vector3 centerScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
            // Get the offset between the center of the screen and the object
            if (offsetFromCamera == Vector3.zero) {
                offsetFromCamera = transform.position - centerScreen;
            }

            // Calculate the target position using the camera's forward direction
            targetPosition = centerScreen + Camera.main.transform.forward * offsetFromCamera.magnitude;

            Debug.Log("Screen center " + centerScreen);
            Debug.Log("Offset " + offsetFromCamera);
            Debug.Log("Target Position " + targetPosition);
        } else {
            if (offsetFromCamera != Vector3.zero) {
                offsetFromCamera = Vector3.zero;
            }
        }

        // Smoothly move the object towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSpeed);
    }
}
