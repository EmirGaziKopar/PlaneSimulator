using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlierAIAlternative : MonoBehaviour
{

    public float speed = 10f; // Uçaðýn hareket hýzý
    public float rotationSpeed = 5f; // Uçaðýn rotasyon hýzý
    public float maxAltitude = 100f; // Maksimum irtifa
    public float minAltitude = 10f; // Minimum irtifa
    public float altitudeChangeInterval = 10f; // Ýrtifa deðiþtirme aralýðý
    public float turnInterval = 5f; // Dönüþ aralýðý
    public Transform[] waypoints; // Rotada takip edilecek noktalar

    private float currentAltitude;
    private bool isTurningRight = true;
    private float turnTimer = 0f;
    private float altitudeChangeTimer = 0f;
    private int currentWaypointIndex = 0;
    private bool isReturning = false;

    private void Start()
    {
        // Uçaðýn baþlangýç irtifasý
        currentAltitude = minAltitude;
    }

    private void Update()
    {
        // Zamanlayýcýlarý güncelle
        turnTimer += Time.deltaTime;
        altitudeChangeTimer += Time.deltaTime;

        // Eðer geri dönüþ modundaysan, sonraki hedef rotaya doðru hareket et
        if (isReturning)
        {
            MoveTowardsWaypoint(waypoints[currentWaypointIndex - 1].position);
        }
        else
        { // Rotadaki bir sonraki hedefe doðru hareket et
            MoveTowardsWaypoint(waypoints[currentWaypointIndex].position);
        }

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

        // Hedef rotaya ulaþtýysan, bir sonraki hedef rotaya geç
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 3f && !isReturning)
        {
            currentWaypointIndex++;

            // Eðer son rotadaysan, geri dönüþ moduna geç
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2; // Son rotayý tekrar ziyaret etmek için bir önceki rotaya geç
                isReturning = true;
            }
        }

        // Geri dönüþ modunda, baþlangýç konumuna ulaþtýysan, rotanýn baþýna dön
        if (Vector3.Distance(transform.position, waypoints[0].position) < 1f && isReturning)
        {
            currentWaypointIndex =0;
            isReturning = false;
        }
    }


    // Belirtilen noktaya doðru hareket et
    /*
    private void MoveTowardsWaypoint(Vector3 waypointPosition)
    {
        // Belirtilen noktaya doðru yönel
        Vector3 direction = (waypointPosition - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        // Uçaðý hareket ettir
        transform.position += transform.forward * speed * Time.deltaTime;

        // Ýrtifayý ayarla
        Vector3 currentPos = transform.position;
        currentPos.y = currentAltitude;
        transform.position = currentPos;
    }
    */
    //Bu fonksiyon, belirtilen noktaya doðru yönelirken, y ekseninde ayný irtifada kalýr ve sadece x ve z eksenlerinde hareket eder.
    //Uçaðý hareket ettirmek için x ve z koordinatlarý güncellenir, yükseklik koordinatý ise sabit kalýr. Son olarak, uçaðýn irtifasý da ayarlanýr.
    private void MoveTowardsWaypoint(Vector3 waypointPosition)
    {
        // Belirtilen noktaya doðru yönel
        Vector3 direction = (new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z) - transform.position).normalized;
        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        // Uçaðý hareket ettir
        Vector3 newPosition = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, currentAltitude, newPosition.z);

        // Ýrtifayý ayarla
        Vector3 currentPos = transform.position;
        currentPos.y = currentAltitude;
        transform.position = currentPos;
    }

}

