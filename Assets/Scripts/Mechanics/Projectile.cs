using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifeTime;

    //Meant to be modified by the object creating the projectile
    // eg. the fire script
    [HideInInspector]
    public Vector2 initVel; 
    void Start()
    {
        if (lifeTime <= 0) lifeTime = 2.0f;
        
        GetComponent<Rigidbody2D>().velocity = initVel;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision) 
    {
       if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
