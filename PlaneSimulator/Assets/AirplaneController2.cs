using UnityEngine;

public class AirplaneController2 : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed = 10f;
    public float rotateSpeed = 2f;
    public float turnAngle = 30f;

    private int currentWayPointIndex = 0;
    private bool takingOff = true;
    private bool landing = false;

    void Update()
    {
        if (takingOff)
        {
            TakeOff();
        }
        else if (landing)
        {
            Land();
        }
        else
        {
            MoveTowardsWayPoint();
        }
    }

    void TakeOff()
    {
        float takeOffHeight = 50f;
        float takeOffSpeed = 5f;

        // Uçak yüksekliði belirlenen hýza ulaþana kadar artýrýlýr.
        if (transform.position.y < takeOffHeight)
        {
            transform.Translate(Vector3.up * takeOffSpeed * Time.deltaTime);
        }
        else
        {
            takingOff = false;
        }
    }

    void MoveTowardsWayPoint()
    {
        // Way pointlere doðru ilerlenir.
        Vector3 targetPosition = new Vector3(wayPoints[currentWayPointIndex].position.x, transform.position.y, wayPoints[currentWayPointIndex].position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Way pointe doðru yön döndürülür.
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Way pointe yaklaþýldýðýnda bir sonraki way pointe ilerlenir.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWayPointIndex++;

            // Eðer tüm way pointler tamamlandýysa, uçak iniþe geçer.
            if (currentWayPointIndex >= wayPoints.Length)
            {
                landing = true;
            }
        }
        else
        {
            // Uçaðýn dönüþü ayarlanýr.
            Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
            Quaternion targetTurnRotation = Quaternion.LookRotation(targetDirection);

            float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

            if (Mathf.Abs(angle) > turnAngle)
            {
                float turnDirection = Mathf.Sign(angle);
                transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, turnDirection * turnAngle, 0), rotateSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetTurnRotation, rotateSpeed * Time.deltaTime);
            }
        }
    }

    void Land()
    {
        float landingHeight = 0f;
        float landingSpeed = 3f;

        // Uçak yüksekliði sýfýra indirilir.
        if (transform.position.y > landingHeight)
        {
            transform.Translate(Vector3.down * landingSpeed * Time.deltaTime);
        }
        else
        {
            // Uçak yere oturduðunda destroy edilir.
            Destroy(gameObject);
        }
    }
}
