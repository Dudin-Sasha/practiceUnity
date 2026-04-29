using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VR_move_scriptt : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private CharacterController controller;
    [SerializeField] private float sensitivity = 0.2f; // Чувствительность мыши
    private Vector2 lookInput;
    Vector2 move;
    private float rotationX = 0f;
    private float rotationY = 0f;


    private void Start() {
        controller = GetComponent<CharacterController>();
        //_rb = GetComponent<Rigidbody>();
    }


    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context) {
        lookInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate() {

        transform.position += new Vector3(move.x, 0f, move.y);


        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.rotation = targetRotation;
    }
}
