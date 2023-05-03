using UnityEngine;
using System.Collections.Generic;


public class AirplaneController3 : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public List<Transform> waypoints;

    private int currentWaypointIndex = 0;
    private Vector3 targetPosition;
    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer bileþeni oluþturulur.
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = waypoints.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Waypoint'ler LineRenderer bileþeni üzerinde çizilir.
        for (int i = 0; i < waypoints.Count; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }

        // Baþlangýçta ilk hedef way point olarak atanýr.
        targetPosition = waypoints[0].position;
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Way pointe doðru ilerlenir.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Way pointe doðru yön döndürülür.
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        Quaternion targetRotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationToTarget, rotationSpeed * Time.deltaTime);

        // Way pointe yaklaþýldýðýnda bir sonraki way pointe ilerlenir.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
            targetPosition = waypoints[currentWaypointIndex].position;
        }

        // LineRenderer bileþeni güncellenir.
        for (int i = 0; i < waypoints.Count; i++)
        {
            lineRenderer.SetPosition(i, waypoints[i].position);
        }
    }
}
