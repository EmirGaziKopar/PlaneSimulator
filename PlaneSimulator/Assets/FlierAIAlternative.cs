using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierAIAlternative : MonoBehaviour
{

    public float speed = 10f; // U�a��n hareket h�z�
    public float rotationSpeed = 5f; // U�a��n rotasyon h�z�
    public float maxAltitude = 100f; // Maksimum irtifa
    public float minAltitude = 10f; // Minimum irtifa
    public float altitudeChangeInterval = 10f; // �rtifa de�i�tirme aral���
    public float turnInterval = 5f; // D�n�� aral���
    public Transform[] waypoints; // Rotada takip edilecek noktalar

    private float currentAltitude;
    private bool isTurningRight = true;
    private float turnTimer = 0f;
    private float altitudeChangeTimer = 0f;
    private int currentWaypointIndex = 0;
    private bool isReturning = false;

    private void Start()
    {
        // U�a��n ba�lang�� irtifas�
        currentAltitude = minAltitude;
    }

    private void Update()
    {
        // Zamanlay�c�lar� g�ncelle
        turnTimer += Time.deltaTime;
        altitudeChangeTimer += Time.deltaTime;

        // E�er geri d�n�� modundaysan, sonraki hedef rotaya do�ru hareket et
        if (isReturning)
        {
            MoveTowardsWaypoint(waypoints[currentWaypointIndex - 1].position);
        }
        else
        { // Rotadaki bir sonraki hedefe do�ru hareket et
            MoveTowardsWaypoint(waypoints[currentWaypointIndex].position);
        }

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

        // Hedef rotaya ula�t�ysan, bir sonraki hedef rotaya ge�
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 3f && !isReturning)
        {
            currentWaypointIndex++;

            // E�er son rotadaysan, geri d�n�� moduna ge�
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2; // Son rotay� tekrar ziyaret etmek i�in bir �nceki rotaya ge�
                isReturning = true;
            }
        }

        // Geri d�n�� modunda, ba�lang�� konumuna ula�t�ysan, rotan�n ba��na d�n
        if (Vector3.Distance(transform.position, waypoints[0].position) < 1f && isReturning)
        {
            currentWaypointIndex =0;
            isReturning = false;
        }
    }


    // Belirtilen noktaya do�ru hareket et
    /*
    private void MoveTowardsWaypoint(Vector3 waypointPosition)
    {
        // Belirtilen noktaya do�ru y�nel
        Vector3 direction = (waypointPosition - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        // U�a�� hareket ettir
        transform.position += transform.forward * speed * Time.deltaTime;

        // �rtifay� ayarla
        Vector3 currentPos = transform.position;
        currentPos.y = currentAltitude;
        transform.position = currentPos;
    }
    */
    //Bu fonksiyon, belirtilen noktaya do�ru y�nelirken, y ekseninde ayn� irtifada kal�r ve sadece x ve z eksenlerinde hareket eder.
    //U�a�� hareket ettirmek i�in x ve z koordinatlar� g�ncellenir, y�kseklik koordinat� ise sabit kal�r. Son olarak, u�a��n irtifas� da ayarlan�r.
    private void MoveTowardsWaypoint(Vector3 waypointPosition)
    {
        // Belirtilen noktaya do�ru y�nel
        Vector3 direction = (new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z) - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        // U�a�� hareket ettir
        Vector3 newPosition = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, currentAltitude, newPosition.z);

        // �rtifay� ayarla
        Vector3 currentPos = transform.position;
        currentPos.y = currentAltitude;
        transform.position = currentPos;
    }

}

