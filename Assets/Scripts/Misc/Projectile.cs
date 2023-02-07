using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifetime;

    [HideInInspector]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(gameObject, lifetime);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, lifetime);
        }
        else
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(gameObject); 
        }
    }
}
