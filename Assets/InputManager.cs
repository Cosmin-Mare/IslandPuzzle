using UnityEngine;

public class InputManager : MonoBehaviour{
    private PlayerControls playerControls;

    public static InputManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    public Vector2 GetMovement() {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public bool IsJumpPressed() {
        return playerControls.Player.Jump.triggered;
    }
}
