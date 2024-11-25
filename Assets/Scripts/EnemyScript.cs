using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public TowerScript tower;

    Vector3 origin;
    int counter = 0;
    float health = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = tower.GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = circle(origin, 5, 1f) + 2 * Vector3.up;
        //transform.rotation = Quaternion.LookRotation(origin - transform.position);
        move();

        counter++;
    }

    Vector3 circle(Vector3 origin, float radius, float angularVelocity)
    {
        float av = angularVelocity == 0 ? 1 : angularVelocity / 50;
        Vector3 unitCircle = new Vector3(math.cos((av * counter) / math.PI2), 0, math.sin((av * counter) / math.PI2));
        return origin + radius * unitCircle;
    }

    void move()
    {
        Vector3 loc = transform.position;
        // calculate new forces
        float dx = 5;
        float dy = 0;
        float dz = 0; // 10 * math.sin((50 / math.PI) * loc.x);
        Vector3 force = new Vector3(dx, dy, dz);
        //float constant = 1 / force.magnitude;
        //force += -1 * constant *  (origin + loc);
        force *= 0.02f;

        // apply forces
        transform.position += force;
    }

    void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() is not null)
        {
            takeDamage(collision.gameObject.GetComponent<BulletScript>().damage);
            Destroy(collision.gameObject);
        }
    }
}
