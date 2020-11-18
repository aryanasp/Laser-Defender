using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : SpaceShip
{

    // config params
    List<Transform> wayPoints;


    //cached
    public List<Transform> WayPoints { get => wayPoints; set => wayPoints = value; }
    


    //state
    Vector2 nextPos;
    int destIndex;

    

    

    void Awake()
    {
        InitializeWayPointsList();
    }

    private void InitializeWayPointsList()
    {
        WayPoints = new List<Transform>();
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        direction = Vector2.down;
        FireCooldown = Random.Range((float)0.8 * FireCooldown, (float)1.2 * FireCooldown);
        transform.position = WayPoints[0].position;
        nextPos = transform.position;
        destIndex = 0;
        StartCoroutine(FireContinously());
    }

    // Update is called once per frame
    protected override void Update()
    {   
        Move();
        base.Update();
    }


    private void Move()
    {

        if ((Vector2)transform.position == nextPos)
        {
            destIndex++;
            if (destIndex < WayPoints.Count)
            {
                nextPos = WayPoints[destIndex].position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, nextPos, MovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcessHit(other);
        if (other.gameObject.tag == "Player")
        {
            Health -= CrashDamage;
        }
    }

    private void ProcessHit(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Laser laser = other.gameObject.GetComponent<Laser>();
            laser.Hit();
            Health -= laser.Damage;
        }
    }
}
