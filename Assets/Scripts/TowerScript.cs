using UnityEditor;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject building;
    public GameObject barrel;
    public GameObject bulletPrefab;
    public Transform target;
    

    Vector3 origin;
    Vector3 direction;
    Quaternion targetRotation;

    int counter = 0;
    float rotationSpeed = 50f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // target = GameObject.FindGameObjectWithTag("Enemy").transform;

        origin = building.transform.position;
        origin.y += 0.5f;

        // barrel.GetComponent<Transform>().position = origin;
    }

    // Update is called once per frame
    void Update()
    {
        direction = target.position - barrel.transform.position;

        targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        barrel.transform.position = origin + 0.9f * direction.normalized;

    }

    private void FixedUpdate()
    {
        if (target != null && counter == 0)
        {
            Fire(1000f);
        }
        counter++;
        counter %= 256;
    }

    void Fire(float projectileSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefab, barrel.transform.position + 0.5f * direction.normalized, barrel.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * projectileSpeed);
    }
}
