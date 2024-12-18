using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private float _moveSpeed = 7f;

    private Camera _mainCamera;

    public float MoveSpeed {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    private Vector2 _movement;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    private void Update() {
        RotateTowardsMouse();
    }

    private void FixedUpdate() {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        if (_movement.magnitude > 1f) {
            _movement = _movement.normalized;
        }

        _rb.velocity = _movement * _moveSpeed;
    }

    private void RotateTowardsMouse() {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Interpolation applied for smoother rotaiton style
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 5f);
    }
}
