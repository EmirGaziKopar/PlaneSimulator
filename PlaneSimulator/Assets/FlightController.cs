using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public float speed = 10.0f; // u�a��n h�z�
    public float rotationSpeed = 100.0f; // u�a��n d�n�� h�z�
    public float lift = 10.0f; // u�a��n kald�rma g�c�
    public float gravity = 10.0f; // yer�ekimi ivmesi
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            // u�ak yerdeyken kontrol ediliyorsa
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // u�a�� yukar� kald�rmak i�in kuvvet uygulama
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = lift;
            }
        }
        else
        {
            // u�ak havadayken kontrol ediliyorsa
            moveDirection = transform.forward * speed;

            // u�a�� yere �ekmek i�in yer�ekimi uygulama
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // u�a�� d�nd�rme
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        // u�a�� hareket ettirme
        controller.Move(moveDirection * Time.deltaTime);
    }
}
