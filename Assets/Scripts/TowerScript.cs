using UnityEditor;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject building;
    public GameObject barrel;
    public GameObject bulletPrefab;
    public Transform target;
    public float projectileSpeed = 10000f;

    Vector3[] positions = new Vector3[3];
    Vector3[] velocities = new Vector3[2];
    Vector3 acceleration;
    Vector3 origin;
    Vector3 direction;
    Vector3 correctedPosition;
    Quaternion targetRotation;

    int counter = 0;
    float rotationSpeed = 50f;
    float timeOfFlight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = building.transform.position;
        origin.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AddToList(positions, target.position);
        AddToList(velocities, (positions[0] - positions[1]) / Time.deltaTime);
        timeOfFlight = Vector3.Distance(barrel.transform.position, target.position) / projectileSpeed;
        acceleration = (velocities[0] - velocities[1]) / Time.deltaTime;

        correctedPosition = target.position + (timeOfFlight * velocities[0]) + (timeOfFlight / 2 * timeOfFlight * acceleration);

        direction = correctedPosition - barrel.transform.position;

        targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        barrel.transform.position = origin + 0.9f * direction.normalized;

    }

    private void FixedUpdate()
    {
        if (target != null && counter == 0)
        {
            Fire();
        }
        counter++;
        counter %= 128;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.transform.position + 0.5f * direction.normalized, barrel.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * projectileSpeed);
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
