using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    public float timeBeforeExplosion = 3;
    public float explosionRadius = 2.5f;
    public ParticleSystem explosionParticle;

    void Start()
    {
        StartCoroutine("Explode");
    }


    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);

        ParticleSystem p = Instantiate(explosionParticle,transform.position,Quaternion.identity);
        p.Play();
        Destroy(p.gameObject,1);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ocean")
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
