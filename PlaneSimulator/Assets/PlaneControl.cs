using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float infinty;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        if (Input.GetKey(KeyCode.I))
        {
            infinty += 0.01f;
        }
        if (Input.GetKey(KeyCode.K))
        {
            infinty -= 0.01f;
        }

        //transform.rotation = Quaternion.Euler(infinty, rotation*180, 0);
        transform.Rotate(infinty, rotation, 0);
    }
}


