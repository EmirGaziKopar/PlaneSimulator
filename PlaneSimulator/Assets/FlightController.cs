using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public float speed = 10.0f; // uçaðýn hýzý
    public float rotationSpeed = 100.0f; // uçaðýn dönüþ hýzý
    public float lift = 10.0f; // uçaðýn kaldýrma gücü
    public float gravity = 10.0f; // yerçekimi ivmesi
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            // uçak yerdeyken kontrol ediliyorsa
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // uçaðý yukarý kaldýrmak için kuvvet uygulama
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = lift;
            }
        }
        else
        {
            // uçak havadayken kontrol ediliyorsa
            moveDirection = transform.forward * speed;

            // uçaðý yere çekmek için yerçekimi uygulama
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // uçaðý döndürme
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        // uçaðý hareket ettirme
        controller.Move(moveDirection * Time.deltaTime);
    }
}
