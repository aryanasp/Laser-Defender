using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    //config params
    [SerializeField]
    int laserProjectileSpeed;

    //cached
    Rigidbody2D myRigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myRigidbody2D.velocity = new Vector2(0, laserProjectileSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
