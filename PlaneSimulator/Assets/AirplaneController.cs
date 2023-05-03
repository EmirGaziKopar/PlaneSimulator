using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 10f;
    public float rollSpeed = 10f;
    public float pitchSpeed = 10f;
    public float yawSpeed = 10f;
    public float maxPitchAngle = 45f;
    public float maxRollAngle = 45f;

    private Rigidbody rb;
    private float pitch = 0f;
    private float roll = 0f;


    /*Bu script, Rigidbody bile�enine sahip bir u�a�� kontrol etmek i�in kullan�labilir. 
     * U�a�� ileri hareket ettirmek i�in, hareket y�n�n� transform.forward vekt�r�ne ayarlayarak Rigidbody'nin h�z�n� ayarlayarak yap�l�r. 
     * U�ak ayr�ca pitch, roll ve yaw hareketlerine izin verir, bu hareketler, u�a��n rotasyonunu de�i�tirerek yap�l�r. 
     * Kod, bunlar� Input.GetAxis () fonksiyonlar�ndan gelen girdilere g�re hesaplar. 
     * U�a��n maksimum pitch ve roll a��lar�, maxPitchAngle ve maxRollAngle de�i�kenleri kullan�larak s�n�rlan�r.
    */
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float yaw = Input.GetAxis("Yaw");

        // Rotate the airplane based on input axes
        pitch += vertical * pitchSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);

        // Roll the airplane based on input axes
        roll = Mathf.Lerp(roll, -horizontal * maxRollAngle, Time.deltaTime * rollSpeed);
        transform.rotation *= Quaternion.Euler(0f, 0f, roll);

        // Move the airplane forward
        rb.velocity = transform.forward * moveSpeed;
    }
}

