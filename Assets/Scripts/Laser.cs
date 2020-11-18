using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    //config params
    float laserProjectileSpeed;
    int damage = 100;
    public int Damage { get => damage;}
    public Vector2 Direction { get => direction; set => direction = value; }
    public float LaserProjectileSpeed { get => laserProjectileSpeed; set => laserProjectileSpeed = value; }


    //cached
    Rigidbody2D myRigidbody2D;
    Vector2 direction;

    //state


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        myRigidbody2D.velocity = LaserProjectileSpeed * Direction ;
    }


    public void Hit()
    {
        Destroy(gameObject);
    }

}
