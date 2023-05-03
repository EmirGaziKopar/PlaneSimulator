using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlierAIAlternative2 : MonoBehaviour
{
    public List<Transform> wayPoints; // The list of waypoints to follow
    public float moveSpeed = 10f; // The speed at which the plane moves
    public float rotateSpeed = 2f; // The speed at which the plane rotates
    public float turnAngle = 30f; // The angle at which the plane turns
    public float turnInterval = 2f; // The interval at which the plane turns

    private int currentWayPointIndex = 0; // The index of the current waypoint
    private bool isTakingOff = true; // Whether the plane is taking off or not
    private bool isLanding = false; // Whether the plane is landing or not
    private Vector3 takeoffPosition; // The takeoff position of the plane
    private Quaternion takeoffRotation; // The takeoff rotation of the plane

    void Start()
    {
        takeoffPosition = transform.position;
        takeoffRotation = transform.rotation;
    }

    void Update()
    {
        if (isTakingOff)
        {
            MoveTowardsPosition(takeoffPosition);
            if (Vector3.Distance(transform.position, takeoffPosition) < 0.1f)
            {
                isTakingOff = false;
            }
        }
        else if (isLanding)
        {
            MoveTowardsPosition(takeoffPosition);
            if (Vector3.Distance(transform.position, takeoffPosition) < 0.1f)
            {
                isLanding = false;
            }
        }
        else
        {
            if(currentWayPointIndex < wayPoints.Count)
            {
                MoveTowardsWayPoint(wayPoints[currentWayPointIndex]);
            }
            
        }
    }

    // Moves the plane towards a given position
    void MoveTowardsPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
    }

    // Moves the plane towards a given waypoint
    void MoveTowardsWayPoint(Transform wayPoint)
    {
        Vector3 targetPosition = new Vector3(wayPoint.position.x, transform.position.y, wayPoint.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // If the plane is close enough to the waypoint, move on to the next waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Count)
            {
                isLanding = true;
            }
        }

        // If enough time has passed, turn the plane in a random direction
        if (Time.time % turnInterval == 0)
        {
            float randomAngle = Random.Range(-turnAngle, turnAngle);
            Quaternion turnRotation = Quaternion.Euler(0f, randomAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, turnRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
