using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    // config params
    private float movementSpeed;
    List<Transform> wayPoints;
    public List<Transform> WayPoints { get => wayPoints; set => wayPoints = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }


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
    void Start()
    {
        transform.position = WayPoints[0].position;
        nextPos = transform.position;
        destIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        Move();   
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

}
