using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public TowerScript tower;


    Vector3 origin;
    int counter = 0;
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
        float dx = -1 * loc.z;
        float dy = 2 - loc.y;
        float dz = loc.x;
        Vector3 force = new Vector3(dx, dy, dz);
        //float constant = 1 / force.magnitude;
        //force += -1 * constant *  (origin + loc);
        force *= 0.02f;

        // apply forces
        transform.position += force;
    }

}
