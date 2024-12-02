using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    int counter;

    Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        counter++;
        if (counter >= 500)
        {
            Destroy(gameObject);
        }
    }

}
