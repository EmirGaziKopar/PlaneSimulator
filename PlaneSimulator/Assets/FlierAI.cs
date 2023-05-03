using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierAI : MonoBehaviour
{
    public float speed = 10f; // Uçaðýn hareket hýzý
    public float rotationSpeed = 5f; // Uçaðýn rotasyon hýzý
    public float maxAltitude = 100f; // Maksimum irtifa
    public float minAltitude = 10f; // Minimum irtifa
    public float altitudeChangeInterval = 10f; // Ýrtifa deðiþtirme aralýðý
    public float turnInterval = 5f; // Dönüþ aralýðý

    private float currentAltitude;
    private bool isTurningRight = true;
    private float turnTimer = 0f;
    private float altitudeChangeTimer = 0f;

    private void Start()
    {
        turnInterval = Random.Range(1, 10);
        // Uçaðýn baþlangýç irtifasý
        currentAltitude = minAltitude;
    }

    private void Update()
    {
        // Zamanlayýcýlarý güncelle
        turnTimer += Time.deltaTime;
        altitudeChangeTimer += Time.deltaTime;

        // Ýrtifa deðiþtirme aralýðýna ulaþtýysan irtifayý deðiþtir
        if (altitudeChangeTimer >= altitudeChangeInterval)
        {
            currentAltitude = Mathf.Clamp(currentAltitude + Random.Range(-10f, 10f), minAltitude, maxAltitude);
            altitudeChangeTimer = 0f;
        }

        // Dönüþ aralýðýna ulaþtýysan dönüþ yap
        if (turnTimer >= turnInterval)
        {
            isTurningRight = !isTurningRight;
            turnTimer = 0f;
        }

        // Uçaðý ileri hareket ettir
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Uçaðý yukarý veya aþaðý hareket ettir
        float altitudeDelta = currentAltitude - transform.position.y;
        transform.Translate(Vector3.up * Time.deltaTime * altitudeDelta * 0.1f);

        // Uçaðý döndür
        float rotationAmount = isTurningRight ? rotationSpeed : -rotationSpeed;
        transform.Rotate(Vector3.up, rotationAmount * Time.deltaTime);
    }
}




