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
        _moveAction = _playerInput.actions[MoveAction];
    }

    private void Update() {
        Movement = _moveAction.ReadValue<Vector2>();
    }
}