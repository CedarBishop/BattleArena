using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force;
    public int damage;
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * force);
    }

    public void Fire (Quaternion quaternion)
    {
        transform.rotation = quaternion;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Block>())
        {
            collision.gameObject.GetComponent<Block>().LoseHealth(damage);
        }
        Destroy(gameObject);
    }
}
