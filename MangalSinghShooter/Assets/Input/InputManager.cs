using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    private const string MoveAction = "Move";

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        if(_playerInput == null) {
            Debug.LogError("PlayerInput Component not found on GameObject");
        }
        _moveAction = _playerInput.actions[MoveAction];
        if(_moveAction == null) {
            Debug.LogError($"MoveAction '{MoveAction}' not found");
        }
    }

    private void Update() {
        Movement = _moveAction.ReadValue<Vector2>();
    }
}
