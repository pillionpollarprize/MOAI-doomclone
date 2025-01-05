using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject explosionPrefab;
    public float speed = 20f;
    public float lifetime = 2f;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", lifetime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider col)
    {
            var health = col.gameObject.GetComponent<Health>();
            {
                if (health != null) // if the bullet hit something that has health
                {
                    if (col.gameObject.CompareTag("Player")) 
                    {
                       // since player has 2 colliders, it doubles the damage. its a bandaid fix but it works for now
                       // sometimes the damage only applies 1 time instead of 2 times, but i guess it can create some unpredictable
                       // RNG for funsies. a feature if you will
                       health.TakeDamage(damage/2);
                    }
                    else
                    {
                        health.TakeDamage(damage);
                    }
                }
                else // if the bullet hit something
                {
                    SelfDestruct();
                }
            }
            SelfDestruct();
        
    }
    void SelfDestruct()
    {
        //Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
