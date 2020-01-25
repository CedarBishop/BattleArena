using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    public float explosionRadius = 2.5f;
    void Start()
    {
        StartCoroutine("Explode");
    }


    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetComponent<Block>())
                {
                    Destroy(colliders[i].gameObject);
                }
            }
        }
       

        Destroy(gameObject);
    }
}
