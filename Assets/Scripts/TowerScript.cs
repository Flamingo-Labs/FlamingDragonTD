using UnityEditor;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject building;
    public GameObject barrel;
    public GameObject bulletPrefab;
    public GameObject targetMarker;
    public Transform target;

    Vector3[] positions = new Vector3[3];
    Vector3[] velocities = new Vector3[2];
    Vector3 acceleration;
    Vector3 toTarget;
    Vector3 origin;
    Vector3 direction;
    Vector3 correctedPosition;
    Quaternion targetRotation;

    float projectileSpeed = 50f;
    float[] c = new float[3];
    int counter = 0;
    float timeToHit;
    float discriminant;
    float rotationSpeed = 500f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = building.transform.position;
        origin.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        targetMarker.transform.position = correctedPosition;
        direction = correctedPosition - barrel.transform.position;

        targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        barrel.transform.position = origin + 0.9f * direction.normalized;
    }

    private void FixedUpdate()
    {
        AddToList(positions, target.position);
        AddToList(velocities, (target.position - positions[1]) / Time.deltaTime);
        acceleration = (velocities[0] - velocities[1]) / Time.deltaTime;

        toTarget = target.position - barrel.transform.position;

        c[0] = Vector3.Dot(velocities[0], velocities[0]) - projectileSpeed * projectileSpeed;
        c[1] = 2 * Vector3.Dot(velocities[0], toTarget);
        c[2] = Vector3.Dot(toTarget, toTarget);

        discriminant = c[1] * c[1] - 4 * c[0] * c[2];

        if (discriminant >= 0)
        {
            timeToHit = Mathf.Max((-c[1] + Mathf.Sqrt(discriminant)) / (2 * c[0]), (-c[1] - Mathf.Sqrt(discriminant))) / (2 * c[0]);
            Debug.Log("TargetVelocity: " + velocities[0].magnitude);
            Debug.Log("Calculated Time of Flight: " + timeToHit);
            Debug.Log("Projectile Velocity: " + projectileSpeed);
            if (timeToHit > 0)
            {
                correctedPosition = target.position + 10 * velocities[0];
            }
            else
            {
                correctedPosition = 100 * Vector3.up;
            }
        }

        else
        {
            correctedPosition = 100 * Vector3.up;
        }

        if (target != null && counter == 0)
        {
            Fire();
        }
        counter++;
        counter %= 128;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.transform.position + 0.5f * direction.normalized, targetRotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = projectileSpeed * direction.normalized;
    }

    void AddToList(Vector3[] arr, Vector3 val)
    {
        Vector3 temp;
        for (int i = 0; i < arr.Length; i++)
        {
            temp = arr[i];
            arr[i] = val;
            val = temp;
        }
    }
}
