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


    /*Bu script, Rigidbody bileþenine sahip bir uçaðý kontrol etmek için kullanýlabilir. 
     * Uçaðý ileri hareket ettirmek için, hareket yönünü transform.forward vektörüne ayarlayarak Rigidbody'nin hýzýný ayarlayarak yapýlýr. 
     * Uçak ayrýca pitch, roll ve yaw hareketlerine izin verir, bu hareketler, uçaðýn rotasyonunu deðiþtirerek yapýlýr. 
     * Kod, bunlarý Input.GetAxis () fonksiyonlarýndan gelen girdilere göre hesaplar. 
     * Uçaðýn maksimum pitch ve roll açýlarý, maxPitchAngle ve maxRollAngle deðiþkenleri kullanýlarak sýnýrlanýr.
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

