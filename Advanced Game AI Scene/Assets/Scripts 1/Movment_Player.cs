using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public float sprintSpeed = 10f; // Velocidad de carrera del jugador
    public float rotationSpeed = 5f; // Velocidad de rotación de la cámara
    public Camera playerCamera; // Referencia a la cámara del jugador

    private bool isSprinting = false; // Variable para controlar si el jugador está corriendo o no
    public Animator animator;

    // Update se llama una vez por frame
    void Update()
    {
        // Movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula la dirección de movimiento relativa al jugador
        Vector3 movement = transform.TransformDirection(new Vector3(horizontalInput, 0f, verticalInput).normalized);

        if (movement != Vector3.zero)
        {
            // Rotar al jugador en dirección al movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        float speed = isSprinting ? sprintSpeed : moveSpeed;
        Vector3 moveAmount = movement * speed * Time.deltaTime;
        transform.Translate(moveAmount, Space.World); // Mueve al jugador en el espacio mundial

        // Cambio entre sprint y caminar
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            animator.SetBool("Sprint", true);

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            animator.SetBool("Sprint", false);
        }

        // Control de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Rotar el jugador horizontalmente
        transform.Rotate(Vector3.up * mouseX, Space.World); // Rotación en el espacio mundial
    }
}
