using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierAI : MonoBehaviour
{
    public float speed = 10f; // U�a��n hareket h�z�
    public float rotationSpeed = 5f; // U�a��n rotasyon h�z�
    public float maxAltitude = 100f; // Maksimum irtifa
    public float minAltitude = 10f; // Minimum irtifa
    public float altitudeChangeInterval = 10f; // �rtifa de�i�tirme aral���
    public float turnInterval = 5f; // D�n�� aral���

    private float currentAltitude;
    private bool isTurningRight = true;
    private float turnTimer = 0f;
    private float altitudeChangeTimer = 0f;

    private void Start()
    {
        turnInterval = Random.Range(1, 10);
        // U�a��n ba�lang�� irtifas�
        currentAltitude = minAltitude;
    }

    private void Update()
    {
        // Zamanlay�c�lar� g�ncelle
        turnTimer += Time.deltaTime;
        altitudeChangeTimer += Time.deltaTime;

        // �rtifa de�i�tirme aral���na ula�t�ysan irtifay� de�i�tir
        if (altitudeChangeTimer >= altitudeChangeInterval)
        {
            currentAltitude = Mathf.Clamp(currentAltitude + Random.Range(-10f, 10f), minAltitude, maxAltitude);
            altitudeChangeTimer = 0f;
        }

        // D�n�� aral���na ula�t�ysan d�n�� yap
        if (turnTimer >= turnInterval)
        {
            isTurningRight = !isTurningRight;
            turnTimer = 0f;
        }

        // U�a�� ileri hareket ettir
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // U�a�� yukar� veya a�a�� hareket ettir
        float altitudeDelta = currentAltitude - transform.position.y;
        transform.Translate(Vector3.up * Time.deltaTime * altitudeDelta * 0.1f);

        // U�a�� d�nd�r
        float rotationAmount = isTurningRight ? rotationSpeed : -rotationSpeed;
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
    }
}




